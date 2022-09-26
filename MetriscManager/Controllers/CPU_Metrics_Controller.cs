using System.Linq.Expressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetriscManager.Controllers
{
    [Route("api/CPU")]
    [ApiController]
    public class CPU_Metrics_Controller : ControllerBase
    {
        [HttpGet("all/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetCpuMetricsFromAll([FromQuery] TimeSpan timeFrom, [FromQuery] TimeSpan timeTo)
        {
            
            return Ok();
        }

        [HttpGet("agentID/{agentid}/from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetCpuMetricsFromAgent
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
