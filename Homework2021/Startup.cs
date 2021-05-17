using Homework2021.EFORM;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Homework2021.DAO;
using Homework2021.Logic;
using Homework2021.Logic.Interface;
using Homework2021.DAO.Interface;
using Homework2021.DTO;
using Homework2021.Content;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;
using Homework2021.Content.Mail;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IO;

namespace Homework2021
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        //����log�bconsole�U�[��ϥΡA���ݮɥi����
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(x =>
            {
                x.AddPolicy("AllowAny", build =>
                {
                    build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddDbContextPool<UserWorkContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("DataBase"))
                       .UseLoggerFactory(loggerFactory);
            });
            #region(�ҥ�JWT����)
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.IncludeErrorDetails = true;
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            NameClaimType = ClaimTypes.NameIdentifier,
                            ValidateIssuer = true,
                            ValidIssuer = this.Configuration.GetValue<string>("JwtSettings:Issuer"),
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = false,
                            ClockSkew = TimeSpan.Zero,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
                        };
                    });
            #endregion
            #region(�s�WQuartz�A��)
            //�s�WQuartz�A��
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //�s�W�ڪ�Job
            services.AddSingleton<DemoJob>();
            services.AddSingleton(
                 new RS_JobSchedule(jobType: typeof(DemoJob), cronExpression: "0/5 * * * * ?")
           );
            #endregion
            services.Configure<EF_MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
            services.AddSingleton<JwtHelpers>();     
            services.AddHostedService<MD_QuartzHostedService>();
            
            services.AddScoped<IUserRepository,Dao_UserManage>();
            services.AddScoped<IGroupRepository,Dao_GroupManage>();
            services.AddScoped<ICustomerRepository,Dao_Customer>();
            services.AddScoped<IUserService, MD_User>();
            services.AddScoped<IGroupService,MD_Group>();            
            services.AddScoped<ICustomerService,MD_Customer>();
            services.AddScoped<MD_UserrefGroup>();

            services.AddControllers();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "�ڪ�API",
                    Description = "�@��²�檺API�A�o�S����A�ɤO���n",
                    TermsOfService = new Uri("https://example.com/terms"),                   
                    
                });
            });
           

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "�ڪ�API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
