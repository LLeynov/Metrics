using MetricsManager.Models.BasedMetrics;

namespace MetricsManager.Models.Responses
{
    public class HDDMetricsResponse
    {
        public int AgentId { get; set; }

        public HDDMetric[] Metrics { get; set; }
    }
}
