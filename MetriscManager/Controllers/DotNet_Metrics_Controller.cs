using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetriscManager.Controllers
{
    [Route("api/DOTNET")]
    [ApiController]
    public class DotNet_Metrics_Controller : ControllerBase
    {
        [HttpGet("all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetDotNetMetricsFromAll([FromQuery] TimeSpan timeFrom, [FromQuery] TimeSpan timeTo)
        {
            return Ok();
        }

        [HttpGet("agentID/{agentid}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetDotNetMetricsFromAgent
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
