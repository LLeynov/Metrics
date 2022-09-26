using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetriscManager.Controllers
{
    [Route("api/RAM")]
    [ApiController]
    public class RAM_Metrics_Controller : ControllerBase
    {
        [HttpGet("all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetRAMMetricsFromAll([FromQuery] TimeSpan timeFrom, [FromQuery] TimeSpan timeTo)
        {
            return Ok();
        }

        [HttpGet("agentID/{agentid}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetRAMMetricsFromAgent
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
