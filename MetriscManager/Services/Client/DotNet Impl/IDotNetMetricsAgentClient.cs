using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;

namespace MetricsManager.Services.Client.DotNet_Impl
{
    public interface IDotNetMetricsAgentClient
    {
        DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest request);
    }
}
