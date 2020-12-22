using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBank.ScheduledJobs.CronJobConfig.Abstract
{
    public interface IScheduleConfig<T>
    {
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
