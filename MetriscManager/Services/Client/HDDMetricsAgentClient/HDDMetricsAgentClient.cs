using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using Newtonsoft.Json;

namespace MetricsManager.Services.Client.HDDMetricsAgentClient
{
    public class HDDMetricsAgentClient : IHDDMetricsAgentClient
    {
        private readonly AgentPool _agentPool;
        private readonly HttpClient _httpClient;

        public HDDMetricsAgentClient(AgentPool agentPool, HttpClient httpClient)
        {
            _agentPool = agentPool;
            _httpClient = httpClient;
        }

        public HDDMetricsResponse GetHDDMetrics(HDDMetricsRequest request)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAdress}api/metrics/hdd/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                HDDMetricsResponse hddMetricsResponse =
                    (HDDMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(HDDMetricsResponse));
                hddMetricsResponse.AgentId = request.AgentId;
                return hddMetricsResponse;
            }

            return null;
        }
    }
}
