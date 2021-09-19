using System;
using System.Collections.Generic;
using System.Threading;

namespace Core.Services
{
    public class SchedulerService
    {
        private static SchedulerService _instance;
        private List<Timer> _timers = new List<Timer>();
        public static SchedulerService Instance => _instance ?? (_instance = new SchedulerService());
        private SchedulerService() { }
        
        public void ScheduleTask(DateTime when, TimeSpan interval, Action task)
        {
            DateTime now = DateTime.Now;

            TimeSpan timeToGo = when - now;
            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }
            var timer = new Timer(_ =>
            {
                task.Invoke();
            }, null, timeToGo, interval);

            _timers.Add(timer);
        }
    }
}
