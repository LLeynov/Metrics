using System.Diagnostics;
using MetricsAgent.Models;
using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using Quartz;

namespace MetricsAgent.Jobs.Target_Jobs
{
    public class HDD_Metrics_Job : IJob
    {
        private readonly IHDDMetricsRepository _hddMetricsRepository;
        private PerformanceCounter _hddCounter;

        public HDD_Metrics_Job(IHDDMetricsRepository hddMetricsRepository)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            //Получаем значение занятости CPU
            float hddUsageInPercents = _hddCounter.NextValue();

            //Узнаём,когда сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //Записываем что-то посредством репозитория
            _hddMetricsRepository.Create(new HDD_Metrics
            {
                Time = (long)time.TotalSeconds,
                Value = (int)hddUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
