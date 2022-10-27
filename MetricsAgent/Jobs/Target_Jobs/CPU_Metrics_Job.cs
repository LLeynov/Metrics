using System.Diagnostics;
using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace MetricsAgent.Jobs.Target_Jobs
{
    public class CPU_Metrics_Job : IJob
    {
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private PerformanceCounter _cpuCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public CPU_Metrics_Job(ICpuMetricsRepository cpuMetricsRepository, IServiceScopeFactory serviceScopeFactory)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time",
                "_Total");
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task Execute(IJobExecutionContext context)
        {

            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var cpuMetricsRepository = serviceScope.ServiceProvider.GetService<ICpuMetricsRepository>();
                try
                {
                    var cpuUsageInPercents = _cpuCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    //Debug.WriteLine($"{time} > {cpuUsageInPercents}");
                    cpuMetricsRepository.Create(new Models.CPU_Metrics
                    {
                        Value = (int)cpuUsageInPercents,
                        Time = (long)time.TotalSeconds
                    });
                }
                catch (Exception ex)
                {

                }
            }


            return Task.CompletedTask;
        }
    }
}
