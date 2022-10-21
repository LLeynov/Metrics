using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class RAM_Metrics_Controller_Tests
    {
        private RAM_Metrics_Controller _ramMetricsController;

        public RAM_Metrics_Controller_Tests()
        {
            _ramMetricsController = new RAM_Metrics_Controller();
        }

        [Fact]
        public void GetRAMMetricsFromAgent()
        {
            //Готовим данные для теста
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            //Исполнение самого метода
            var result = _ramMetricsController.GetRAMMetricsFromAgent(agentId, fromTime, toTime);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetRAMMetricsFromAll()
        {
            //Готовим данные для теста
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            //Исполнение самого метода
            var result = _ramMetricsController.GetRAMMetricsFromAll(fromTime, toTime);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
