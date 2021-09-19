using Core.Models;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IVehicleProcessing
    {
        VehicleResult UpdateVehicleInfo(VehicleParamsExtend vehicleParams);
        VehicleResult InsertVehicleInfo(VehicleParams vehicleParams);
        VehicleResult GetRandomReverseVehicleInfo();
        VehicleResult GetVehicleInfo(Guid vehicleId);
        IEnumerable<VehicleResult> FindVehicle(FindVehicleParams vehicleParams);
        /// <summary>
        /// Джоб обнуления случайной цены
        /// </summary>
        void NullifyRandomPrice();
    }
}