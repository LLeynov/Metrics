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




        //[HttpPut("enable/{agentId}")]
        //public IActionResult EnableAgent([FromRoute] int agentId)
        //{
        //    if (_agentPool.Agent.ContainsKey(agentId))
        //        _agentPool.Agent[agentId].Enabeled = true;
        //    return Ok();
        //}
        //[HttpPut("disable/{agentId}")]
        //public IActionResult DisableAgent([FromRoute] int agentId)
        //{
        //    if (_agentPool.Agent.ContainsKey(agentId))
        //        _agentPool.Agent[agentId].Enabeled = false;
        //    return Ok();
        //}

        [HttpGet("get")]
        public ActionResult<AgentInfo[]> GetAllAgents()
        {
            return Ok(_agentPool.Get());
        }
    }
}
