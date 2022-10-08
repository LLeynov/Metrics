using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Options;

namespace MetricsAgent.Services.Impl
{
    public class NetworkMetricsRepository : INetWorkMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public NetworkMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(Network_Metrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM networkmetrics WHERE id=@id", new
            {
                id = id
            });
        }

        public IList<Network_Metrics> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<Network_Metrics>("SELECT * FROM networkmetrics").ToList();
        }

        public Network_Metrics GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.QuerySingle<Network_Metrics>("SELECT * FROM networkmetrics WHERE id=@id", new
            {
                id = id
            });
        }

        public IList<Network_Metrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<Network_Metrics>(
                "SELECT * FROM networkmetrics where time >= @timeFrom and time <= @timeTo", new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(Network_Metrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id;", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
    }
}
