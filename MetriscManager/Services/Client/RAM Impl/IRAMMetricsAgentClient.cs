using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;

namespace MetricsManager.Services.Client.RAM_Impl
{
    public interface IRAMMetricsAgentClient
    {
        RAMMetricsResponse GetRamMetrics(RAMMetricsRequest request);
    }
}
