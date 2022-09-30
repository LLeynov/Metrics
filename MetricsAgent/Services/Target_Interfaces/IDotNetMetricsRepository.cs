using MetricsAgent.Models;

namespace MetricsAgent.Services.Target_Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNet_Metrics>
    {
        IList<DotNet_Metrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}

