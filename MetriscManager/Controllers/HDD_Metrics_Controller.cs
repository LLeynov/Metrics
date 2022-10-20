using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/HDD")]
    [ApiController]
    public class HDD_Metrics_Controller : ControllerBase
    {
        [HttpGet("all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetHDDMetricsFromAll([FromQuery] TimeSpan timeFrom, [FromQuery] TimeSpan timeTo)
        {
            return Ok();
        }

        [HttpGet("agentID/{agentid}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetHDDMetricsFromAgent
        (
            [FromQuery] int agentid,
            [FromQuery] TimeSpan timeFrom,
            [FromQuery] TimeSpan timeTo
        )
        {
            return Ok();
        }
    }
}
