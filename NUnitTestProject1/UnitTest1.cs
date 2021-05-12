using Homework2021;
using Homework2021.Logic.Interface;
using Homework2021.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.IO;

namespace NUnitTestProject1
{
    public class Tests
    {
        private IGroupService _Group;

        [SetUp]
        public void Setup()
        {
            var AppsettingsPath = Directory.GetCurrentDirectory() + "/appsettings.json";
            var configuration = new ConfigurationBuilder().AddJsonFile(AppsettingsPath).Build();
            var server = new TestServer(new WebHostBuilder().UseConfiguration(configuration).UseStartup<Startup>());
            _Group = server.Host.Services.GetRequiredService<IGroupService>();

        }

        [Test]
        public void TestGetGroupCount(int? page, string GroupName)
        {
            Assert.IsFalse(_Group.GetGroup(page,GroupName).GetAwaiter().GetResult().Count > 0);
        }
        [Test]
        public void TestCreateGroupCount(EF_Group DataEntry)
        {
            Assert.IsFalse(_Group.CreateGroup(DataEntry).GetAwaiter().GetResult().Count > 0);
        }
        [Test]
        public void TestUpdateGroupCount(EF_Group DataEntry)
        {
            Assert.IsFalse(_Group.UpdateGroup(DataEntry).GetAwaiter().GetResult().Count > 0);
        }
        [Test]
        public void TestDeleteGroupCount(int userid, int groupid)
        {
            Assert.IsFalse(_Group.DeleteGroup(userid,groupid).GetAwaiter().GetResult().Count > 0);
        }
    }
}