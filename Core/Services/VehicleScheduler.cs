using Core.Interfaces;
using System;

namespace Core.Services
{
    public class VehicleScheduler
    {
        public static void Start(IVehicleProcessing vehicleProcessing)
        {
            SchedulerService.Instance.ScheduleTask(System.DateTime.Now, TimeSpan.FromSeconds(30), vehicleProcessing.NullifyRandomPrice);
        }
    }
}
