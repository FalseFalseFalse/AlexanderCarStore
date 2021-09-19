using System;

namespace Core.Models
{
    public class VehicleResult : VehicleParamsBase
    {
        public virtual Guid? Guid { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
