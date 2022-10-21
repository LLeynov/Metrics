using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Options;

namespace MetricsAgent.Services.Impl
{
    public class RAMMetricsRepository : IRAMMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public RAMMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }


        public void Create(RAM_Metrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }
        public IList<RAM_Metrics> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<RAM_Metrics>("SELECT * FROM rammetrics").ToList();
        }

        public RAM_Metrics GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.QuerySingle("SELECT * FROM rammetrics WHERE id=@id", new
            {
                id = id
            });
        }

        public IList<RAM_Metrics> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<RAM_Metrics>("SELECT * FROM rammetrics where time >= @fromTime and time <= @toTime", new
            {
                fromTime = fromTime.TotalSeconds,
                toTime = toTime.TotalSeconds
            }).ToList();
        }
    }
}
 