using System.Text.Json.Serialization;

namespace Core.Models
{
    public class StatusStatistic
    {
        public string Status { get; set; }

        [JsonPropertyName("Count")]
        public int CountType { get; set; }
    }
}
