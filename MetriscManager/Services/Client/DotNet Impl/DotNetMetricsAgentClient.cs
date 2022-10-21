using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using MetricsManager.Services.Client.DotNet_Impl;
using Newtonsoft.Json;

namespace MetricsManager.Services.Client.DotNet_Impk
{
    public class DotNetMetricsAgentClient : IDotNetMetricsAgentClient
    {
        private readonly AgentPool _agentPool;
        private readonly HttpClient _httpClient;

        public DotNetMetricsAgentClient(AgentPool agentPool, HttpClient httpClient)
        {
            _agentPool = agentPool;
            _httpClient = httpClient;
        }

        public DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest request)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAdress}api/metrics/dotnet/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                DotNetMetricsResponse dotnetMetricsResponse =
                    (DotNetMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(DotNetMetricsResponse));
                dotnetMetricsResponse.AgentId = request.AgentId;
                return dotnetMetricsResponse;
            }

            return null;
        }
    }
}
