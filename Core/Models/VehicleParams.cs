using System;

namespace Core.Models
{
    public class VehicleParams
    {
        public string VehicleType { get; set; }
        public string Marque { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public int EnginePowerBhp { get; set; }
        public int TopSpeedMph { get; set; }
        public DateTime DatePurchase { get; set; }
        public decimal CostUsd { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
