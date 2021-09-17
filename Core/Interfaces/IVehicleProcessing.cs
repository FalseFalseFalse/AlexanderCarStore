using Core.Models;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IVehicleProcessing
    {
        public VehicleResult SetVehicleInfo(VehicleParams vehicleParams);
        public VehicleResult GetRandomReverseVehicleInfo();
        public VehicleResult GetVehicleInfo(Guid guid);
        public IEnumerable<VehicleResult> FindVehicle(FindVehicleParams vehicleParams);
    }
}