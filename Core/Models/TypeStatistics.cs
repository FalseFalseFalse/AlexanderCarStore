using System.Text.Json.Serialization;

namespace Core.Models
{
    public class TypeStatistics
    {
        public string VehicleType { get; set; }

        [JsonPropertyName("Count")]
        public int CountType { get; set; }
    }
}
