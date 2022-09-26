using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class Network_Metrics_Controller_Tests
    {
        private Network_Metrics_Controller _networkMetricsController;

        public Network_Metrics_Controller_Tests()
        {
            _networkMetricsController = new Network_Metrics_Controller();
        }

        [Fact]
        public void GetNetWorkMetricsFromAgent()
        {
            //Готовим данные для теста
            int agentId = 1;
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение самого метода
            var result = _networkMetricsController.GetNetWorkMetricsFromAgent(agentId, timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetNetWorkMetricsFromAll()
        {
            //Готовим данные для теста
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение самого метода
            var result = _networkMetricsController.GetNetWorkMetricsFromAll(timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
