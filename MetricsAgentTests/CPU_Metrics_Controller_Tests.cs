using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsAgentTests
{
    public class CPU_Metrics_Controller_Tests
    {
        private CPU_Metrics_Controller _cpuMetricsController;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<CPU_Metrics_Controller>> mockLogger;

        public CPU_Metrics_Controller_Tests()
        {
            mock = new Mock<ICpuMetricsRepository>();
            mockLogger = new Mock<ILogger<CPU_Metrics_Controller>>();
            _cpuMetricsController = new CPU_Metrics_Controller(mock.Object,mockLogger.Object);
        }

        [Fact]
        public void GetCPUMetrics()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository =>
                repository.Create(It.IsAny<CPU_Metrics>())).Verifiable();

            // Выполняем действие на контроллере
            var result = _cpuMetricsController.Create(new
                MetricsAgent.Models.Requests.CpuMetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта параметре

            mock.Verify(repository => repository.Create(It.IsAny<CPU_Metrics>()),
                Times.AtMostOnce());
        }
    }
}
