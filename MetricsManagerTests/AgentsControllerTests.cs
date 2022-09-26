using MetricsManager.Controllers;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class AgentsControllerTests
    {
        private AgentsController _agentsController;

        public AgentsControllerTests()
        {
            _agentsController = new AgentsController(new AgentPool());
        }

        [Fact]
        public void RegisterAgent()
        {
            //Готовим данные для метода
            AgentInfo agentInfo = new AgentInfo();
            agentInfo.AgentId = 1;
            agentInfo.AgentAdress = new Uri("http://localhost/5040");
            agentInfo.Enabeled = true;


            //Исполнение метода
            var result = _agentsController.RegisterAgent(agentInfo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgent()
        {
            //Готовим данные для метода
            AgentInfo agentInfo = new AgentInfo();
            agentInfo.AgentId = 1;
            agentInfo.AgentAdress = new Uri("http://localhost/5040");
            agentInfo.Enabeled = false;

            //Исполнение метода
            var result = _agentsController.EnableAgent(1);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgent()
        {
            //Готовим данные для метода
            AgentInfo agentInfo = new AgentInfo();
            agentInfo.AgentId = 1;
            agentInfo.AgentAdress = new Uri("http://localhost/5040");
            agentInfo.Enabeled = true;

            //Исполнение метода
            var result = _agentsController.DisableAgent(1);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetAllAgents()
        {
            //Готовим данные для метода
            AgentInfo agentInfo = new AgentInfo();
            agentInfo.AgentId = 1;
            agentInfo.AgentAdress = new Uri("http://localhost/5040");
            agentInfo.Enabeled = true;
            _agentsController.RegisterAgent(agentInfo);

            //Исполнение метода
            var result = _agentsController.GetAllAgents();

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}