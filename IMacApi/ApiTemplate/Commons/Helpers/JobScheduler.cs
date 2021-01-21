using Quartz;
using Quartz.Impl;
using System;
using System.Configuration;

namespace ApiTemplate.Commons.Helpers
{
    public class JobScheduler
    {
        //private readonly IScheduler _scheduler;

        //public JobScheduler(IScheduler scheduler)
        //{
        //    _scheduler = scheduler;
        //}
        public static void Start()
        {
            IScheduler _scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            _scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailJobDueDate>().Build();
            int hour = int.Parse(ConfigurationManager.AppSettings["SchedulerHour"]);
            int minute = int.Parse(ConfigurationManager.AppSettings["SchedulerMinute"]);

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(hour, minute))
                  )
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }
    }
}