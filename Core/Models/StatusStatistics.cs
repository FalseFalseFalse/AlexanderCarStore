using System.Text.Json.Serialization;

namespace Core.Models
{
    public class StatusStatistics
    {
        public string Status { get; set; }

        [JsonPropertyName("Count")]
        public int CountType { get; set; }
    }
}
