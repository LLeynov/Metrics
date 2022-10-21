using MetricsManager.Models.BasedMetrics;

namespace MetricsManager.Models.Responses
{
    public class RAMMetricsResponse
    {
        public int AgentId { get; set; }

        public RAMMetric[] Metrics { get; set; }
    }
}
