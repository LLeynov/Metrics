using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsAgentTests
{
    public class Network_Metrics_Controller_Tests
    {
        private Network_Metrics_Controller _networkMetricsController;
        private Mock<INetWorkMetricsRepository> mock;
        private Mock<ILogger<Network_Metrics_Controller>> mockLogger;
        public Network_Metrics_Controller_Tests()
        {
            mock = new Mock<INetWorkMetricsRepository>();
            mockLogger = new Mock<ILogger<Network_Metrics_Controller>>();
            _networkMetricsController = new Network_Metrics_Controller(mock.Object,mockLogger.Object);
        }

        [Fact]
        public void GetNetworkMetrics()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository =>
                repository.Create(It.IsAny<Network_Metrics>())).Verifiable();

            // Выполняем действие на контроллере
            var result = _networkMetricsController.Create(new
                MetricsAgent.Models.Requests.NetworkMetricsCreateRequest
                {
                    Time = TimeSpan.FromSeconds(1),
                    Value = 50
                });

            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта параметре

            mock.Verify(repository => repository.Create(It.IsAny<Network_Metrics>()),
                Times.AtMostOnce());
        }
    }
}
