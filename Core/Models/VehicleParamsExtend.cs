using System;

namespace Core.Models
{
    public class VehicleParamsExtend : VehicleParams
    {
        public virtual Guid? Guid { get; set; }
    }
}
