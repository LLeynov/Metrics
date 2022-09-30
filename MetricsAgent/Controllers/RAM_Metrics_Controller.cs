using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    
    [Route("api/metrics/RAM")]
    [ApiController]
    public class RAM_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<RAM_Metrics_Controller> _logger;
        private readonly IRAMMetricsRepository _ramMetricsRepository;

        public RAM_Metrics_Controller(IRAMMetricsRepository ramMetrics, ILogger<RAM_Metrics_Controller> logger)
        {
            _ramMetricsRepository = ramMetrics;
            _logger = logger;
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] RAMMetricsCreateRequest request)
        {
            _logger.LogInformation("Create RAM metric.");
            _ramMetricsRepository.Create(new Models.RAM_Metrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<RAM_Metrics>> GetRamMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get RAM metrics.");
            return Ok(_ramMetricsRepository.GetByTimePeriod(timeFrom,timeTo));
        }
    }
}
