using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/NETWORK")]
    [ApiController]
    public class Network_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<Network_Metrics_Controller> _logger;
        private readonly INetWorkMetricsRepository _netWorkMetricsRepository;

        public Network_Metrics_Controller(INetWorkMetricsRepository networkMetrics, ILogger<Network_Metrics_Controller> logger)
        {
            _netWorkMetricsRepository = networkMetrics;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricsCreateRequest request)
        {
            _logger.LogInformation("Create Network metric.");
            _netWorkMetricsRepository.Create(new Models.Network_Metrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<Network_Metrics>> GetNetworkMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get Network metrics.");
            return Ok(_netWorkMetricsRepository.GetByTimePeriod(timeFrom,timeTo));
        }
    }
}
