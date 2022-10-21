using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using MetricsManager.Services.Client.HDDMetricsAgentClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/hdd")]
    [ApiController]
    public class HDD_Metrics_Controller : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private AgentPool _agentPool;
        private IHDDMetricsAgentClient _hddMetricsAgentClient;

        public HDD_Metrics_Controller(IHttpClientFactory httpClientFactory, AgentPool agentPool,IHDDMetricsAgentClient hddMetricsAgentClient)
        {
            _httpClientFactory = httpClientFactory;
            _agentPool = agentPool;
            _hddMetricsAgentClient = hddMetricsAgentClient;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<HDDMetricsResponse> GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(_hddMetricsAgentClient.GetHDDMetrics(new HDDMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }));
        }

        [HttpGet("agent-old/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<HDDMetricsResponse> GetHDDMetricsFromAgentOld(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == agentId);
            if (agentInfo == null)
                return BadRequest();

            string requestStr =
                $"{agentInfo.AgentAdress}api/metrics/hdd/from/{fromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{toTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpClient httpClient = _httpClientFactory.CreateClient();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(3000); // 3 сек

            HttpResponseMessage response = httpClient.Send(httpRequestMessage, cancellationTokenSource.Token);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                HDDMetricsResponse hddMetricsResponse =
                    (HDDMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(HDDMetricsResponse));
                hddMetricsResponse.AgentId = agentId;
                return Ok(hddMetricsResponse);
            }
            return BadRequest();
        }


        [HttpGet("all/from/{fromTime}/to/{toTime}")]
        public IActionResult GetHDDMetricsFromAll([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }


    }
}
