﻿using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.Content
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }
        public void ReturnJob(IJob job)
        {

        }
    }
}
