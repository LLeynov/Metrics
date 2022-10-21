using MetricsAgent.Models;

namespace MetricsAgent.Services.Target_Interfaces
{
    public interface IHDDMetricsRepository : IRepository<HDD_Metrics>
    {
        IList<HDD_Metrics> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime);
    }
}
