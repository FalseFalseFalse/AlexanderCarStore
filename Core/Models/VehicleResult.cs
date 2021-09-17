using System;

namespace Core.Models
{
    public class VehicleResult : VehicleParams
    {
        public DateTime DateInsert { get; set; }
        public DateTime DateUpdate { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}
