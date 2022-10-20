using MetricsAgent.Models.DTO;

namespace MetricsAgent.Models.Requests
{
    public class GetCpuMetricsResponse
    {
        public List<CPU_MetricsDTO> Metrics { get; set; }
    }
}
