using MetriscManager.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace MetricsManagerTests
{
    public class CPU_Metrics_Tests
    {
        private CPU_Metrics_Controller _cpuMetricsController;

        public CPU_Metrics_Tests()
        {
            _cpuMetricsController = new CPU_Metrics_Controller();
        }

        [Fact]
        public void GetCpuMetricsFromAgent_ReturnOK()
        {
            //Готовим данные для теста
            int agentId = 1;
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение самого метода
            var result = _cpuMetricsController.GetCpuMetricsFromAgent(agentId, timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetCpuMetricsFromAll_ReturnOK()
        {
            //Готовим данные для теста
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение метода
            var result = _cpuMetricsController.GetCpuMetricsFromAll(timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
