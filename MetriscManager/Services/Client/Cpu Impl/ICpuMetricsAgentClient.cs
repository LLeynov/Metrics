using MetricsManager.Models.Requests;

namespace MetricsManager.Services.Client.Cpu_Impl
{
    public interface ICpuMetricsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request);
    }
}
