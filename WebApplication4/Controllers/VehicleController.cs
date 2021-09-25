using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contoller.Controllers
{
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleProcessing _vehicleProcessing;

        public VehicleController(IVehicleProcessing vehicleProcessing)
        {
            _vehicleProcessing = vehicleProcessing;
        }

        [HttpPost]
        [Route("InsertVehicle")]
        public VehicleResult InsertVehicle([FromBody] VehicleParams vehicleParams)
        {
            var result = _vehicleProcessing.InsertVehicleInfo(vehicleParams);

            return
                new VehicleResult()
                {
                    Guid = result.Guid,
                    VehicleType = result.VehicleType,
                    Marque = result.Marque,
                    Engine = result.Engine,
                    Model = result.Model,
                    DatePurchase = result.DatePurchase,
                    DateInsert = result.DateInsert,
                    DateUpdate = result.DateUpdate,
                    Price = result.Price,
                    TopSpeedMph = result.TopSpeedMph,
                    CostUsd = result.CostUsd,
                    EnginePowerBhp = result.EnginePowerBhp,
                    Status = result.Status
                };
        }

        [HttpPut]
        [Route("UpdateVehicle")]
        public IEnumerable<VehicleResult> UpdateVehicle([FromBody] VehicleParamsExtend vehicleParams)
        {
            var result = _vehicleProcessing.UpdateVehicleInfo(vehicleParams);

            return new VehicleResult[]
            {
                new VehicleResult()
                {
                    Guid = result.Guid,
                    VehicleType = result.VehicleType,
                    Marque = result.Marque,
                    Engine = result.Engine,
                    Model = result.Model,
                    DatePurchase = result.DatePurchase,
                    DateInsert = result.DateInsert,
                    DateUpdate = result.DateUpdate,
                    Price = result.Price,
                    TopSpeedMph = result.TopSpeedMph,
                    CostUsd = result.CostUsd,
                    EnginePowerBhp = result.EnginePowerBhp,
                    Status = result.Status
                }
            }.ToList();
        }

        [HttpGet]
        [Route("GetVehicle")]
        public IEnumerable<VehicleResult> GetVehicle(Guid guid)
        {
            var result = _vehicleProcessing.GetVehicleInfo(guid);

            return new VehicleResult[]
            {
                new VehicleResult()
                {
                    Guid = result.Guid,
                    VehicleType = result.VehicleType,
                    Marque = result.Marque,
                    Engine = result.Engine,
                    Model = result.Model,
                    DatePurchase = result.DatePurchase,
                    DateUpdate = result.DateUpdate,
                    DateInsert = result.DateInsert,
                    Price = result.Price,
                    TopSpeedMph = result.TopSpeedMph,
                    CostUsd = result.CostUsd,
                    EnginePowerBhp = result.EnginePowerBhp,
                    Status = result.Status
                }
            }.ToList();
        }

        [HttpGet]
        [Route("GetRandomReversVehicle")]
        public IEnumerable<VehicleResult> GetRandomReverseVehicle()
        {
            var result = _vehicleProcessing.GetRandomReverseVehicleInfo();

            return new VehicleResult[]
            {
                new VehicleResult()
                {
                    Guid = result.Guid,
                    VehicleType = result.VehicleType,
                    Marque = result.Marque,
                    Engine = result.Engine,
                    Model = result.Model,
                    DatePurchase = result.DatePurchase,
                    DateUpdate = result.DateUpdate,
                    DateInsert = result.DateInsert,
                    Price = result.Price,
                    TopSpeedMph = result.TopSpeedMph,
                    CostUsd = result.CostUsd,
                    EnginePowerBhp = result.EnginePowerBhp,
                    Status = result.Status
                }
            }.ToList();
        }

        [HttpGet]
        [Route("FindVehicle")]
        public IEnumerable<VehicleResult> FindVehicle(FindVehicleParams vehicleParams)
        {
            var result = _vehicleProcessing.FindVehicle(vehicleParams);

            return result;
        }
    }
}