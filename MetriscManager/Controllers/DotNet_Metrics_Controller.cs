using System.Net;
using System.Net.Http;
using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Models.Responses;
using MetricsManager.Services.Client.DotNet_Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/dotnet")]
    [ApiController]
    public class DotNet_Metrics_Controller : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private AgentPool _agentPool;
        private IDotNetMetricsAgentClient _dotNetMetricsAgentClient;

        public DotNet_Metrics_Controller(IHttpClientFactory httpClientFactory, AgentPool agentPool,IDotNetMetricsAgentClient dotNetMetricsAgentClient)
        {
            _httpClientFactory = httpClientFactory;
            _agentPool = agentPool;
            _dotNetMetricsAgentClient = dotNetMetricsAgentClient;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<DotNetMetricsResponse> GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok(_dotNetMetricsAgentClient.GetDotNetMetrics(new DotNetMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }));
        }

        [HttpGet("agent-old/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetricsFromAgentOld
        (
            [FromRoute] int agentId,
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime
        )
        {
            AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == agentId);
            if (agentInfo == null)
                return BadRequest();

            string requestStr =
                $"{agentInfo.AgentAdress}api/metrics/dotnet/from/{fromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{toTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpClient httpClient = _httpClientFactory.CreateClient();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(3000); // 3 сек

            HttpResponseMessage response = httpClient.Send(httpRequestMessage, cancellationTokenSource.Token);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                DotNetMetricsResponse dotNetMetricsResponse =
                    (DotNetMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(DotNetMetricsResponse));
                dotNetMetricsResponse.AgentId = agentId;
                return Ok(dotNetMetricsResponse);
            }
            return BadRequest();
        }


        [HttpGet("all/from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetricsFromAll([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

   
    }
}
