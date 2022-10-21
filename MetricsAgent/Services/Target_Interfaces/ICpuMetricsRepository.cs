using MetricsAgent.Models;

namespace MetricsAgent.Services.Target_Interfaces
{
    public interface ICpuMetricsRepository : IRepository<CPU_Metrics>
    {
        IList<CPU_Metrics> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime);
    }
}
