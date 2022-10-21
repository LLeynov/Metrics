using MetricsAgent.Models.DTO;

namespace MetricsAgent.Models.DTO.Response
{
    public class GetCpuMetricsResponse
    {
        public List<CPU_MetricsDTO> Metrics { get; set; }
    }
}
