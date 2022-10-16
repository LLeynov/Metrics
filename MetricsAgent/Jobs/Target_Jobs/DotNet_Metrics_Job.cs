using System.Diagnostics;
using MetricsAgent.Models;
using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using Quartz;

namespace MetricsAgent.Jobs.Target_Jobs
{
    public class DotNet_Metrics_Job : IJob
    {
        private readonly IDotNetMetricsRepository _dotnetMetricsRepository;
        private PerformanceCounter _dotnetCounter;

        public DotNet_Metrics_Job(IDotNetMetricsRepository dotnetMetricsRepository)
        {
            _dotnetMetricsRepository = dotnetMetricsRepository;
            _dotnetCounter = new PerformanceCounter(".NET CLR Exceptions", "# of Exceps Thrown / sec", "_Global_");
        }
        public Task Execute(IJobExecutionContext context)
        {
            //Получаем значение занятости CPU
            float dotnetUsageInPercents = _dotnetCounter.NextValue();

            //Узнаём,когда сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //Записываем что-то посредством репозитория
            _dotnetMetricsRepository.Create(new DotNet_Metrics
            {
                Time = (long)time.TotalSeconds,
                Value = (int)dotnetUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
