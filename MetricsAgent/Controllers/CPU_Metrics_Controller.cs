using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    
    [Route("api/metrics/CPU")]
    [ApiController]
    public class CPU_Metrics_Controller : ControllerBase
    {
        [HttpGet("from/{timeFrom}/to/{timeTo}")]
        public IActionResult GetCPUMetrics([FromRoute] TimeSpan timeFrom, [FromRoute] TimeSpan timeTo)
        {
            return Ok();
        }
    }
}
