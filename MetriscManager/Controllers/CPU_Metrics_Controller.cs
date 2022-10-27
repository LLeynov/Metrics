using System.Diagnostics;
using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Services.Client.Cpu_Impl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace MetriсsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    public class CPU_Metrics_Controller : ControllerBase
    {

        private IHttpClientFactory _httpClientFactory;
        private AgentPool _agentPool;
        private ICpuMetricsAgentClient _cpuMetricsAgentClient;

        public CPU_Metrics_Controller(IHttpClientFactory httpClientFactory, AgentPool agentPool,ICpuMetricsAgentClient cpuMetricsAgentClient)
        {
            _httpClientFactory = httpClientFactory;
            _agentPool = agentPool;
            _cpuMetricsAgentClient = cpuMetricsAgentClient;
        }
       
        [HttpGet("cpu-get-all-by-id")]
        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<CpuMetricsResponse> GetMetricsFromAgent(
            [FromQuery] int agentId, [FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            return Ok(_cpuMetricsAgentClient.GetCpuMetrics(new CpuMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            }));
        }

        //[HttpGet("agent-old/{agentId}/from/{fromTime}/to/{toTime}")]
        //public ActionResult<CpuMetricsResponse> GetCpuMetricsFromAgentOld(
        //    [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    AgentInfo agentInfo = _agentPool.Get().FirstOrDefault(agent => agent.AgentId == agentId);
        //    if (agentInfo == null)
        //        return BadRequest();

        //    string requestStr =
        //        $"{agentInfo.AgentAdress}api/metrics/cpu/from/{fromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{toTime.ToString("dd\\.hh\\:mm\\:ss")}";
        //    HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
        //    httpRequestMessage.Headers.Add("Accept", "application/json");
        //    HttpClient httpClient = _httpClientFactory.CreateClient();

        //    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        //    cancellationTokenSource.CancelAfter(3000); // 3 сек

        //    HttpResponseMessage response = httpClient.Send(httpRequestMessage, cancellationTokenSource.Token);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string responseStr = response.Content.ReadAsStringAsync().Result;
        //        CpuMetricsResponse cpuMetricsResponse =
        //            (CpuMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(CpuMetricsResponse));
        //        cpuMetricsResponse.AgentId = agentId;
        //        return Ok(cpuMetricsResponse);
        //    }
        //    return BadRequest();
        //}

        [HttpGet("all-cpu")]
        //[HttpGet("all/from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetricsFromAll([FromQuery] TimeSpan fromTime, [FromQuery] TimeSpan toTime)
        {
            return Ok();
        }

    }
}
