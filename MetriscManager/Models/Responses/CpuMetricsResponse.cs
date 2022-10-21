using System.Text.Json.Serialization;
using MetricsManager.Models.BasedMetrics;

namespace MetricsManager.Models.Requests
{
    public class CpuMetricsResponse
    {
        public int AgentId { get; set; }

        [JsonPropertyName("metrics")]
        public CpuMetric[] /*ну не такой уж и пустой (TimeSpan)*/ Metrics { get; set; }
    }
}