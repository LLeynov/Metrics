using System.Diagnostics;
using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using Quartz;

namespace MetricsAgent.Jobs.Target_Jobs
{
    public class CPU_Metrics_Job : IJob
    {
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private PerformanceCounter _cpuCounter;
        public CPU_Metrics_Job(ICpuMetricsRepository cpuMetricsRepository)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time",
                "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"{DateTime.Now}> Cpu_Metrics_Job");

            //Получаем значение занятости CPU
            float cpuUsageInPercents = _cpuCounter.NextValue();

            //Узнаём,когда сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //Записываем что-то посредством репозитория
            _cpuMetricsRepository.Create(new CPU_Metrics
            {
                Time = (long)time.TotalSeconds,
                Value = (int)cpuUsageInPercents
            });
            return Task.CompletedTask;
        }
    }
}
