using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/errors/DotNet")]
    [ApiController]
    public class DotNet_Metrics_Controller : ControllerBase
    {
        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetDotNetMetrics([FromQuery] TimeSpan timeFrom, [FromQuery] TimeSpan timeTo)
        {
            return Ok();
        }
    }
}
