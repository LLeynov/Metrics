using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsAgentTests
{
    public class DotNet_Metrics_Controller_Tests
    {
        private DotNet_Metrics_Controller _donNetMetricsController;
        private Mock<IDotNetMetricsRepository> mock;
        private Mock<ILogger<DotNet_Metrics_Controller>> mockLogger;

        public DotNet_Metrics_Controller_Tests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            mockLogger = new Mock<ILogger<DotNet_Metrics_Controller>>();
            _donNetMetricsController = new DotNet_Metrics_Controller(mock.Object, mockLogger.Object);
        }

        [Fact]
        public void GetDotNetMetrics()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository =>
                repository.Create(It.IsAny<DotNet_Metrics>())).Verifiable();

            // Выполняем действие на контроллере
            var result = _donNetMetricsController.Create(new
                MetricsAgent.Models.Requests.DotNetMetricsCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта параметре

            mock.Verify(repository => repository.Create(It.IsAny<DotNet_Metrics>()),
                Times.AtMostOnce());
        }
    }
}
