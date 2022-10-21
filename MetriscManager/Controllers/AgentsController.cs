using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Options;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private AgentPool _agentPool;

        public AgentsController(AgentPool agentPool)
        {
            _agentPool = agentPool;
        }

        [HttpPost("agentRegistration")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {

            if (agentInfo != null)
            {
                _agentPool.Add(agentInfo);
            }
            return Ok();

        }


        [HttpGet("get")]
        public ActionResult<AgentInfo[]> GetAllAgents()
        {
            return Ok(_agentPool.Get());
        }

    }
}
