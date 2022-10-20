using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using System.Data.SQLite;
using Microsoft.Extensions.Options;
using Dapper;

namespace MetricsAgent.Services.Impl
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;
        public DotNetMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(DotNet_Metrics item)// DATABASE IS LOCKED 
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id", new
            {
                Id = id
            });
        }

        public IList<DotNet_Metrics> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<DotNet_Metrics>("SELECT * FROM dotnetmetrics").ToList();
        }

        public DotNet_Metrics GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.QuerySingle<DotNet_Metrics>("SELECT * FROM dotnetmetrics WHERE id=@id", new
            {
                Id = id
            });
        }

        public IList<DotNet_Metrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<DotNet_Metrics>("SELECT * FROM dotnetmetrics where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(DotNet_Metrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id;", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
    }
}
