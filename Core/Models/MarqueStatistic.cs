﻿using System.Text.Json.Serialization;

namespace Core.Models
{
    public class MarqueStatistic
    {
        public string Marque { get; set; }

        [JsonPropertyName("Count")]
        public int CountType { get; set; }
    }
}
