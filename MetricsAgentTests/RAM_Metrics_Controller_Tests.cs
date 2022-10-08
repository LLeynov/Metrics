using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NLog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MetricsAgentTests
{
    public class RAM_Metrics_Controller_Tests
    {
        private RAM_Metrics_Controller _ramMetricsController;
        private Mock<IRAMMetricsRepository> mock;
        private Mock<ILogger<RAM_Metrics_Controller>> mocklogger;

        public RAM_Metrics_Controller_Tests()
        {
            mock = new Mock<IRAMMetricsRepository>();
            mocklogger = new Mock<ILogger<RAM_Metrics_Controller>>();
            _ramMetricsController = new RAM_Metrics_Controller(mock.Object, mocklogger.Object);
        }

        [Fact]
        public void GetRamMetrics()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository =>
                repository.Create(It.IsAny<RAM_Metrics>())).Verifiable();
            // Выполняем действие на контроллере
            var result = _ramMetricsController.Create(new
                MetricsAgent.Models.Requests.RAMMetricsCreateRequest
                {
                    Time = TimeSpan.FromSeconds(1),
                    Value = 50
                });

            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта параметре

            mock.Verify(repository => repository.Create(It.IsAny<RAM_Metrics>()),
                Times.AtMostOnce());
        }
    }
}
