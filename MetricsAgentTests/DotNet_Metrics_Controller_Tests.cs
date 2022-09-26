using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class DotNet_Metrics_Controller_Tests
    {
        private DotNet_Metrics_Controller _donNetMetricsController;

        public DotNet_Metrics_Controller_Tests()
        {
            _donNetMetricsController = new DotNet_Metrics_Controller();
        }

        [Fact]
        public void GetDotNetMetrics()
        {
            //Готовим данные для метода
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение метода
            var result = _donNetMetricsController.GetDotNetMetrics(timeFrom, timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
