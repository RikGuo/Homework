using Homework2021.Content;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Homework2021.Logic
{
    public class MD_QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<RS_JobSchedule> _jobSchedules;
        public MD_QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IEnumerable<RS_JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory ?? throw new ArgumentNullException(nameof(schedulerFactory));
            _jobFactory = jobFactory ?? throw new ArgumentNullException(nameof(jobFactory));
            _jobSchedules = jobSchedules ?? throw new ArgumentNullException(nameof(jobSchedules));
        }
        public IScheduler Scheduler { get; set; }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);
                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
                jobSchedule.JobStatus = JobStatus.Scheduling;
            }
            await Scheduler.Start(cancellationToken);
            foreach (var jobSchedule in _jobSchedules)
            {
                jobSchedule.JobStatus = JobStatus.Running;
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler.Shutdown(cancellationToken);
            foreach (var jobSchedule in _jobSchedules)
            {

                jobSchedule.JobStatus = JobStatus.Stopped;
            }
        }
        private static IJobDetail CreateJob(RS_JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }
        private static ITrigger CreateTrigger(RS_JobSchedule schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }
    }
}
