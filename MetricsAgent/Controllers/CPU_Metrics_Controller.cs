using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    
    [Route("api/metrics/CPU")]
    [ApiController]
    public class CPU_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<CPU_Metrics_Controller> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;

        public CPU_Metrics_Controller(ICpuMetricsRepository cpuMetrics, ILogger<CPU_Metrics_Controller> logger)
        {
            _cpuMetricsRepository = cpuMetrics;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation("Create CPU metric.");
            _cpuMetricsRepository.Create(new Models.CPU_Metrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<CPU_Metrics>> GetCPUMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get CPU metrics.");
            return Ok(_cpuMetricsRepository.GetByTimePeriod(timeFrom,timeTo));
        }
    }
}
