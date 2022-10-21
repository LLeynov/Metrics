using FluentMigrator.Runner;
using MetricsManager;
using MetricsManager.Converters;
using MetricsManager.Models;
using MetricsManager.Services.Client.Cpu_Impl;
using MetricsManager.Services.Client.DotNet_Impk;
using MetricsManager.Services.Client.DotNet_Impl;
using MetricsManager.Services.Client.HDDMetricsAgentClient;
using MetricsManager.Services.Client.RAM_Impl;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Polly;

namespace MetriscManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddHttpClient();

            #region HTTPMetricClients

            builder.Services.AddHttpClient<ICpuMetricsAgentClient, CpuMetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3,
                    sleepDurationProvider: (attemptCount) => TimeSpan.FromSeconds(attemptCount * 2),
                    onRetry: (response, sleepDuration, attemptCount, context) => {

                        var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
                        logger.LogError(response.Exception != null ? response.Exception :
                                new Exception($"\n{response.Result.StatusCode}: {response.Result.RequestMessage}"),
                            $"(attempt: {attemptCount}) request exception.");
                    }));

            builder.Services.AddHttpClient<IDotNetMetricsAgentClient, DotNetMetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3,
                    sleepDurationProvider: (attemptCount) => TimeSpan.FromSeconds(attemptCount * 2),
                    onRetry: (response, sleepDuration, attemptCount, context) => {

                        var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
                        logger.LogError(response.Exception != null ? response.Exception :
                                new Exception($"\n{response.Result.StatusCode}: {response.Result.RequestMessage}"),
                            $"(attempt: {attemptCount}) request exception.");
                    }));
            
            builder.Services.AddHttpClient<IHDDMetricsAgentClient, HDDMetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3,
                    sleepDurationProvider: (attemptCount) => TimeSpan.FromSeconds(attemptCount * 2),
                    onRetry: (response, sleepDuration, attemptCount, context) => {

                        var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
                        logger.LogError(response.Exception != null ? response.Exception :
                                new Exception($"\n{response.Result.StatusCode}: {response.Result.RequestMessage}"),
                            $"(attempt: {attemptCount}) request exception.");
                    }));

            builder.Services.AddHttpClient<IRAMMetricsAgentClient, RAMMetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3,
                    sleepDurationProvider: (attemptCount) => TimeSpan.FromSeconds(attemptCount * 2),
                    onRetry: (response, sleepDuration, attemptCount, context) => {

                        var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
                        logger.LogError(response.Exception != null ? response.Exception :
                                new Exception($"\n{response.Result.StatusCode}: {response.Result.RequestMessage}"),
                            $"(attempt: {attemptCount}) request exception.");
                    }));

            #endregion

            builder.Services.AddSingleton<AgentPool>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsManager", Version = "v1" });

                // Поддержка TimeSpan
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseAuthorization();
            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}