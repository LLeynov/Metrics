using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/HDD")]
    [ApiController]
    public class HDD_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<HDD_Metrics_Controller> _logger;
        private readonly IHDDMetricsRepository _hddMetricsRepository;

        public HDD_Metrics_Controller(IHDDMetricsRepository hddMetrics, ILogger<HDD_Metrics_Controller> logger)
        {
            _hddMetricsRepository = hddMetrics;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HDDMetricsCreateRequest request)
        {
            _logger.LogInformation("Create HDD metric.");
            _hddMetricsRepository.Create(new Models.HDD_Metrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from /{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<HDD_Metrics>> GetHDDMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get HDD metrics.");
            return Ok(_hddMetricsRepository.GetByTimePeriod(timeFrom,timeTo));
        }
    }

}
