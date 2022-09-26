using MetriscManager.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    public class DotNet_Metrics_Controller_Tests
    {
        private DotNet_Metrics_Controller _dotNetMetricsController;

        public DotNet_Metrics_Controller_Tests()
        {
            _dotNetMetricsController = new DotNet_Metrics_Controller();
        }

        [Fact]
        public void GetDotNetMetricsFromAgent_ReturnOK()
        {
            //Готовим данные для теста
            int agentId = 1;
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение самого метода
            var result = _dotNetMetricsController.GetDotNetMetricsFromAgent(agentId, timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetDotNetMetricsFromAll_ReturnOK()
        {
            //Готовим данные для метода
            int agentId = 1;
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение метода
            var result = _dotNetMetricsController.GetDotNetMetricsFromAll(timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
