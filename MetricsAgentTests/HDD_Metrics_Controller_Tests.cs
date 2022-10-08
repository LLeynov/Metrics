using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsAgentTests
{
    public class HDD_Metrics_Controller_Tests
    {
        private HDD_Metrics_Controller _hddMetricsController;
        private Mock<IHDDMetricsRepository> mock;
        private Mock<ILogger<HDD_Metrics_Controller>> mockLogger;
        public HDD_Metrics_Controller_Tests()
        {
            mock = new Mock<IHDDMetricsRepository>();
            mockLogger = new Mock<ILogger<HDD_Metrics_Controller>>();
            _hddMetricsController = new HDD_Metrics_Controller(mock.Object,mockLogger.Object);
        }

        [Fact]
        public void GetHDDMetrics()
        {
            // Устанавливаем параметр заглушки
            // В заглушке прописываем, что в репозиторий прилетит CpuMetric - объект
            mock.Setup(repository =>
                repository.Create(It.IsAny<HDD_Metrics>())).Verifiable();

            // Выполняем действие на контроллере
            var result = _hddMetricsController.Create(new
                MetricsAgent.Models.Requests.HDDMetricsCreateRequest
                {
                    Time = TimeSpan.FromSeconds(1),
                    Value = 50
                });

            // Проверяем заглушку на то, что пока работал контроллер
            // Вызвался метод Create репозитория с нужным типом объекта параметре

            mock.Verify(repository => repository.Create(It.IsAny<HDD_Metrics>()),
                Times.AtMostOnce());
        }
    }
}
