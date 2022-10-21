using MetricsManager.Models.BasedMetrics;

namespace MetricsManager.Models.Responses
{
    public class DotNetMetricsResponse
    {
        public int AgentId { get; set; }

        public DotNetMetric[] Metrics { get; set; }
    }
}
