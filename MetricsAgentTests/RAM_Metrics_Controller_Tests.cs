using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class RAM_Metrics_Controller_Tests
    {
        private RAM_Metrics_Controller _ramMetricsController;

        public RAM_Metrics_Controller_Tests()
        {
            _ramMetricsController = new RAM_Metrics_Controller();
        }

        [Fact]
        public void GetRamMetrics()
        {
            //Готовим данные для метода
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение метода
            var result = _ramMetricsController.GetRamMetrics(timeFrom,timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
