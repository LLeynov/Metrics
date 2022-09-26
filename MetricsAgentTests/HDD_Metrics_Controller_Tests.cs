using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class HDD_Metrics_Controller_Tests
    {
        private HDD_Metrics_Controller _hddMetricsController;

        public HDD_Metrics_Controller_Tests()
        {
            _hddMetricsController = new HDD_Metrics_Controller();
        }

        [Fact]
        public void GetHDDMetrics()
        {
            //Готовим данные для метода
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение метода
            var result = _hddMetricsController.GetHDDMetrics(timeFrom,timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
