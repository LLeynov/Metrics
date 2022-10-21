using MetricsAgent.Models;

namespace MetricsAgent.Services.Target_Interfaces
{
    public interface IRAMMetricsRepository : IRepository<RAM_Metrics>
    {
        IList<RAM_Metrics> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime);
    }
}
