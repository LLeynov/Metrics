using MetricsAgent.Models;
using MetricsAgent.Services.Target_Interfaces;
using System.Data.SQLite;
using Microsoft.Extensions.Options;
using Dapper;


namespace MetricsAgent.Services.Impl
{
    public class HDDMetricsRepository : IHDDMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;
        public HDDMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(HDD_Metrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM hddmetrics WHERE id=@id", new
            {
                Id = id
            });
        }

        public IList<HDD_Metrics> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<HDD_Metrics>("SELECT * FROM hddmetrics").ToList();
        }

        public HDD_Metrics GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.QuerySingle<HDD_Metrics>("SELECT * FROM hddmetrics WHERE id=@id", new
            {
                Id = id
            });
        }

        public IList<HDD_Metrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            return connection.Query<HDD_Metrics>("SELECT * FROM hddmetrics where time >= @timeFrom and time <= @timeTo", new
            {
                timeFrom = timeFrom.TotalSeconds,
                timeTo = timeTo.TotalSeconds
            }).ToList();
        }

        public void Update(HDD_Metrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("UPDATE hddmetrics SET value = @value, time = @time WHERE id = @id;", new
            {
                value = item.Value,
                time = item.Time, 
                id = item.Id
            });
        }
    }
}
