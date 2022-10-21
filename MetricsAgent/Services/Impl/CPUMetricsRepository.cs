using System.Data.SQLite;
using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using Microsoft.Extensions.Options;
using Dapper;//Жмаппер

namespace MetricsAgent.Services.Impl
{
    public class CPUMetricsRepository : ICpuMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;
        public CPUMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(CPU_Metrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public IList<CPU_Metrics> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<CPU_Metrics>("SELECT * FROM cpumetrics").ToList();
        }

        public CPU_Metrics GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            CPU_Metrics Metrics = connection.QuerySingle<CPU_Metrics>("SELECT * FROM cpumetrics WHERE id=@id", new
            {
                id = id
            });

            return Metrics;
        }

        public IList<CPU_Metrics> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<CPU_Metrics>("SELECT * FROM cpumetrics where time >= @fromTime and time <= @toTime", new
            {
                fromTime = fromTime.TotalSeconds,
                toTime = toTime.TotalSeconds
            }).ToList();
        }

    }
}
