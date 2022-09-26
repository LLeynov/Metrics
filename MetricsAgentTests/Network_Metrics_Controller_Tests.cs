using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class Network_Metrics_Controller_Tests
    {
        private Network_Metrics_Controller _networkMetricsController;

        public Network_Metrics_Controller_Tests()
        {
            _networkMetricsController = new Network_Metrics_Controller();
        }

        [Fact]
        public void GetNetworkMetrics()
        {
            //Готовим данные для метода
            TimeSpan timeFrom = TimeSpan.FromSeconds(0);
            TimeSpan timeTo = TimeSpan.FromSeconds(100);

            //Исполнение метода
            var result = _networkMetricsController.GetNetworkMetrics(timeFrom,timeTo);

            //Эталонный результат
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
