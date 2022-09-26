using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/HDD")]
    [ApiController]
    public class HDD_Metrics_Controller : ControllerBase
    {
        [HttpGet("from /{timeFrom}/to/{timeTo")]
        public IActionResult GetHDDMetrics([FromQuery] TimeSpan timeFrom, [FromQuery] TimeSpan timeTo)
        {
            return Ok();
        }
    }

}
