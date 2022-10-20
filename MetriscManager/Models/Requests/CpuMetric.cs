using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests
{
    public class CpuMetric
    {

        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
