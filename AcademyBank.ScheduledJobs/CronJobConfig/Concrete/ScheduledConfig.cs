using AcademyBank.ScheduledJobs.CronJobConfig.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.ScheduledJobs.CronJobConfig.Concrete
{
    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
