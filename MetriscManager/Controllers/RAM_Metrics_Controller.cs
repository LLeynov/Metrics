using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using MetricsManager.Services.Client.RAM_Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/ram")]
    [ApiController]
    public class RAM_Metrics_Controller : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private AgentPool _agentPool;
        private IRAMMetricsAgentClient _ramMetricsAgentClient;

        public RAM_Metrics_Controller(IHttpClientFactory httpClientFactory, AgentPool agentPool,IRAMMetricsAgentClient ramMetricsAgentClient)
        {
            _httpClientFactory = httpClientFactory;
            _agentPool = agentPool;
            _ramMetricsAgentClient = ramMetricsAgentClient;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<RAMMetricsResponse> GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(_ramMetricsAgentClient.GetRamMetrics(new RAMMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }));
        }

        [HttpGet("agent-old/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<RAMMetricsResponse> GetRamMetricsFromAgentOld(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == agentId);
            if (agentInfo == null)
                return BadRequest();

            string requestStr =
                $"{agentInfo.AgentAdress}api/metrics/ram/from/{fromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{toTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpClient httpClient = _httpClientFactory.CreateClient();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(3000); // 3 сек

            HttpResponseMessage response = httpClient.Send(httpRequestMessage, cancellationTokenSource.Token);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                RAMMetricsResponse ramMetricsResponse =
                    (RAMMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(RAMMetricsResponse));
                ramMetricsResponse.AgentId = agentId;
                return Ok(ramMetricsResponse);
            }
            return BadRequest();
        }

        [HttpGet("all/from/{fromTime}/to/{toTime}")]
        public IActionResult GetRAMMetricsFromAll([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
