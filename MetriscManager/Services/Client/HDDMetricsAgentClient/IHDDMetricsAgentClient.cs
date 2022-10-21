using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;

namespace MetricsManager.Services.Client.HDDMetricsAgentClient
{
    public interface IHDDMetricsAgentClient
    {
        HDDMetricsResponse GetHDDMetrics(HDDMetricsRequest request);
    }
}
