using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleProcessing _vehicleProcessing;

        public VehicleController(ILogger<VehicleController> logger, IVehicleProcessing vehicleProcessing)
        {
            _logger = logger;
            _vehicleProcessing = vehicleProcessing;
        }

        [HttpPost]
        [Route("SetVehicle")] 
        public IEnumerable<VehicleResult> SetVehicle([FromBody] VehicleParams vehicleParams)
        {
            var result = _vehicleProcessing.SetVehicleInfo(vehicleParams);

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
                    EnginePowerBhp = result.EnginePowerBhp
                }
            }.ToList();
        }

        [HttpPost]
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
                    DateInsert = result.DatePurchase,
                    Price = result.Price,
                    TopSpeedMph = result.TopSpeedMph,
                    CostUsd = result.CostUsd,
                    EnginePowerBhp = result.EnginePowerBhp,
                    Status = result.Status
                }
            }.ToList();
        }

        [HttpPost]
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
                    DateInsert = result.DatePurchase,
                    Price = result.Price,
                    TopSpeedMph = result.TopSpeedMph,
                    CostUsd = result.CostUsd,
                    EnginePowerBhp = result.EnginePowerBhp,
                    Status = result.Status
                }
            }.ToList();
        }

        [HttpPost]
        [Route("FindVehicle")] 
        public IEnumerable<VehicleResult> FindVehicle([FromBody] FindVehicleParams vehicleParams)
        {
            var result = _vehicleProcessing.FindVehicle(vehicleParams);

            return result;
        }
    }
}
