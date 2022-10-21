using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using Newtonsoft.Json;

namespace MetricsManager.Services.Client.RAM_Impl
{
    public class RAMMetricsAgentClient : IRAMMetricsAgentClient 
    {
        private readonly AgentPool _agentPool;
        private readonly HttpClient _httpClient; 
        
        public RAMMetricsAgentClient(AgentPool agentPool, HttpClient httpClient)
        {
            _agentPool = agentPool; 
            _httpClient = httpClient;
        }

        public RAMMetricsResponse GetRamMetrics(RAMMetricsRequest request)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAdress}api/metrics/ram/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                RAMMetricsResponse ramMetricsResponse =
                    (RAMMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(RAMMetricsResponse));
                ramMetricsResponse.AgentId = request.AgentId;
                return ramMetricsResponse;
            }

            return null;
        }
    }
}
