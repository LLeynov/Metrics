using MetricsAgent.Services.Impl;
using MetricsAgent.Services.Target_Interfaces;
using System.Data.SQLite;
using AutoMapper;
using Dapper;
using MetricsAgent.Mappings;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpLogging;
using NLog.Web;
using FluentMigrator.Runner;
using MetricsAgent.Jobs;
using MetricsAgent.Jobs.Factories;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using MetricsAgent.Jobs.Target_Jobs;

namespace MetricsAgent
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            #region Configure Options

            builder.Services.Configure<DatabaseOptions>(options =>
             {
                 builder.Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
             });

            #endregion

            #region Configure Mapping

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new
                MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);
            #endregion

            #region Configure FluentMigrator

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddSQLite()
                    .WithGlobalConnectionString(builder.Configuration
                        ["Settings:DatabaseOptions:ConnectionString"].ToString())
                    .ScanIn(typeof(Program).Assembly).For.Migrations()
                ).AddLogging(lb => lb.AddFluentMigratorConsole());

            #endregion

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();

            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            // Add services to the container.
            builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
            builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            builder.Services.AddSingleton<CPU_Metrics_Job>();
            builder.Services.AddSingleton(new JobSchedule(typeof(CPU_Metrics_Job),
                "0/5 * * ? * * *"));

            builder.Services.AddSingleton<DotNet_Metrics_Job>();
            builder.Services.AddSingleton(new JobSchedule(typeof(DotNet_Metrics_Job),
                "0/5 * * ? * * *"));

            builder.Services.AddSingleton<HDD_Metrics_Job>();
            builder.Services.AddSingleton(new JobSchedule(typeof(HDD_Metrics_Job),
                "0/5 * * ? * * *"));

            builder.Services.AddSingleton<RAM_Metrics_Job>();
            builder.Services.AddSingleton(new JobSchedule(typeof(RAM_Metrics_Job),
                "0/5 * * ? * * *"));

            //builder.Services.AddSingleton<Network_Metrics_Job>();
            //builder.Services.AddSingleton(new JobSchedule(typeof(Network_Metrics_Job),
            //    "0/5 * * ? * * *"));

            builder.Services.AddHostedService<QuartzHostedService>();


            builder.Services.AddSingleton<ICpuMetricsRepository, CPUMetricsRepository>();
            builder.Services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            builder.Services.AddSingleton<IHDDMetricsRepository, HDDMetricsRepository>();
            builder.Services.AddSingleton<INetWorkMetricsRepository, NetworkMetricsRepository>();
            builder.Services.AddSingleton<IRAMMetricsRepository, RAMMetricsRepository>();

            //ConfigureSqlLiteConnection();// - Поскольку уже создали таблицу - повторное использование выдаст ИСКЛЮЧЕНИЕ!!!

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

                //Поддержка TimeBAN
                c.MapType<TimeSpan>(() => new OpenApiSchema()
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseHttpLogging();

            var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope serviceScope = serviceScopeFactory.CreateScope())
            {
                var migrationRunner = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                migrationRunner.MigrateUp();
            }

            app.MapControllers();

            app.Run();
        }
    }
}