using System;
using System.Collections.Generic;
using System.Threading;

namespace Core.Services
{
    public class SchedulerService
    {
        private static SchedulerService _instance;
        private List<Timer> timers = new List<Timer>();
        private SchedulerService() { }
        public static SchedulerService Instance => _instance ?? (_instance = new SchedulerService());
        public void ScheduleTask(DateTime when, TimeSpan interval, Action task)
        {
            DateTime now = DateTime.Now;

            TimeSpan timeToGo = when - now;
            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }
            var timer = new Timer(x =>
            {
                task.Invoke();
            }, null, timeToGo, interval);
            timers.Add(timer);
        }
    }
}
