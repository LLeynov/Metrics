using System.Text.Json.Serialization;

namespace MetricsManager.Models.BasedMetrics
{
    public class RAMMetric
    {
        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
