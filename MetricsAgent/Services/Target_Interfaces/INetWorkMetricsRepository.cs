using MetricsAgent.Models;

namespace MetricsAgent.Services.Target_Interfaces
{
    public interface INetWorkMetricsRepository : IRepository<Network_Metrics>
    {
        IList<Network_Metrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
