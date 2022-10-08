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

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM cpumetrics WHERE id=@id", new
            {
                Id = id
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

            return connection.QuerySingle<CPU_Metrics>("SELECT * FROM cpumetrics WHERE id=@id", new
            {
                Id = id
            });
        }

        public IList<CPU_Metrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<CPU_Metrics>("SELECT * FROM cpumetrics where time >= @timeFrom and time <= @timeTo", new
            {
                timeFrom = timeFrom.TotalSeconds,
                timeto = timeTo.TotalSeconds
            }).ToList();
        }

        public void Update(CPU_Metrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id;", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
    }
}
