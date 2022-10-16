using System.Diagnostics;
using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using Quartz;

namespace MetricsAgent.Jobs.Target_Jobs
{
    public class RAM_Metrics_Job : IJob
    {
        private readonly IRAMMetricsRepository _ramMetricsRepository;
        private PerformanceCounter _ramCounter;
        public RAM_Metrics_Job(IRAMMetricsRepository ramMetricsRepository)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            //Получаем значение занятости CPU
            float ramUsageInPercents = _ramCounter.NextValue();

            //Узнаём,когда сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //Записываем что-то посредством репозитория
            _ramMetricsRepository.Create(new RAM_Metrics
            {
                Time = (long)time.TotalSeconds,
                Value = (int)ramUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
