using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class CPU_Metrics_Controller_Tests
    {
        private CPU_Metrics_Controller _cpuMetricsController;

        CPU_Metrics_Controller_Tests()
        {
            _cpuMetricsController = new CPU_Metrics_Controller();
        }

        [Fact]
        public void GetCPUMetrics()
        {
            //Готовим данные для метода
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение метода
            var result = _cpuMetricsController.GetCPUMetrics(timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
