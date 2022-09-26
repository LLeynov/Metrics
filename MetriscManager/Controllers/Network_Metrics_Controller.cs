using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/NETWORK")]
    [ApiController]
    public class Network_Metrics_Controller : ControllerBase
    {
        [HttpGet("all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetNetWorkMetricsFromAll([FromQuery] TimeSpan timeFrom, [FromQuery] TimeSpan timeTo)
        {
            return Ok();
        }

        [HttpGet("agentID/{agentid}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetNetWorkMetricsFromAgent
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
