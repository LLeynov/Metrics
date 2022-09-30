using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/errors/DotNet")]
    [ApiController]
    public class DotNet_Metrics_Controller : ControllerBase
    {
        private readonly ILogger<DotNet_Metrics_Controller> _logger;
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;

        public DotNet_Metrics_Controller(IDotNetMetricsRepository dotnetMetrics, ILogger<DotNet_Metrics_Controller> logger)
        {
            _dotNetMetricsRepository = dotnetMetrics;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricsCreateRequest request)
        {
            _logger.LogInformation("Create DotNet metric.");
            _dotNetMetricsRepository.Create(new Models.DotNet_Metrics
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public ActionResult<IList<DotNet_Metrics>> GetDotNetMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            _logger.LogInformation("Get DotNet metrics.");
            return Ok(_dotNetMetricsRepository.GetByTimePeriod(timeFrom,timeTo));
        }
    }
}
