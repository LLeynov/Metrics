using MetriscManager.Controllers;
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
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение самого метода
            var result = _ramMetricsController.GetRAMMetricsFromAgent(agentId, timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetRAMMetricsFromAll()
        {
            //Готовим данные для теста
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение самого метода
            var result = _ramMetricsController.GetRAMMetricsFromAll(timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
