using static System.Net.WebRequestMethods;

namespace MetricsManager.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            AgentsClient agentClient = new AgentsClient("http://localhost:5240", new HttpClient());

            CPU_Metrics_Client cpuMetricsClient = new CPU_Metrics_Client("http://localhost:5240", new HttpClient());
            RAM_Metrics_Client ramMetricsClient = new RAM_Metrics_Client("http://localhost:5240", new HttpClient());
            HDD_Metrics_Client hddMetricsClient = new HDD_Metrics_Client("http://localhost:5240", new HttpClient());
            DotNet_Metrics_Client dotnetMetricsClient = new DotNet_Metrics_Client("http://localhost:5240", new HttpClient());

            await agentClient.AgentRegistrationAsync(new AgentInfo
            {
                AgentAdress = new Uri("http://localhost:5127"),
                AgentId = 1
            });

            Console.Clear();
            Console.WriteLine("Задачи");
            Console.WriteLine("==============================================");
            Console.WriteLine("1 - Получить метрики за последнюю минуту (CPU)");
            Console.WriteLine("2 - Получить метрики за последнюю минуту (DotNet)");
            Console.WriteLine("3 - Получить метрики за последнюю минуту (HDD)");
            Console.WriteLine("4 - Получить метрики за последнюю минуту (RAM)");
            Console.WriteLine("0 - Завершение работы приложения");
            Console.WriteLine("==============================================");
            Console.WriteLine("Введите номер задачи : ");
            if (int.TryParse(Console.ReadLine(), out int taskNumber))
            {
                switch (taskNumber)
                {
                    case 0:
                        Console.WriteLine("Завершение работы приложения");
                        Console.ReadKey();
                        break;

                    case 1:
                        try
                        {
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
                            CpuMetricsResponse response = await cpuMetricsClient.CpuGetAllByIdAsync(1,
                                fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                toTime.ToString("dd\\.hh\\:mm\\:ss"));

                            foreach (var metric in response.Metrics)
                            {
                                Console.WriteLine(
                                    $"{TimeSpan.FromSeconds(metric.Time).ToString("hh\\:mm\\:ss")} >>> {metric.Value}");
                            }
                            Console.WriteLine("Нажмите любую клавишу");
                            Console.ReadKey(true);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                        break;

                    case 2:
                        try
                        {
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
                            DotNetMetricsResponse response = await dotnetMetricsClient.DotnetGetAllByIdAsync(1,
                                fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                toTime.ToString("dd\\.hh\\:mm\\:ss"));

                            foreach (var metric in response.Metrics)
                            {
                                Console.WriteLine(
                                    $"{TimeSpan.FromSeconds(metric.Time).ToString("hh\\:mm\\:ss")} >>> {metric.Value}");
                            }
                            Console.WriteLine("Нажмите любую клавишу");
                            Console.ReadKey(true);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                        break;

                    case 3:
                        try
                        {
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
                            RAMMetricsResponse response = await ramMetricsClient.RamGetAllByIdAsync(1,
                                fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                toTime.ToString("dd\\.hh\\:mm\\:ss"));

                            foreach (var metric in response.Metrics)
                            {
                                Console.WriteLine(
                                    $"{TimeSpan.FromSeconds(metric.Time).ToString("hh\\:mm\\:ss")} >>> {metric.Value}");
                            }
                            Console.WriteLine("Нажмите любую клавишу");
                            Console.ReadKey(true);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                        break;

                    case 4:
                        try
                        {
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);
                            HDDMetricsResponse response = await hddMetricsClient.HddGetAllByIdAsync(1,
                                fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                toTime.ToString("dd\\.hh\\:mm\\:ss"));

                            foreach (var metric in response.Metrics)
                            {
                                Console.WriteLine(
                                    $"{TimeSpan.FromSeconds(metric.Time).ToString("hh\\:mm\\:ss")} >>> {metric.Value}");
                            }
                            Console.WriteLine("Нажмите любую клавишу");
                            Console.ReadKey(true);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                        break;

                    default:
                        Console.WriteLine("Введите корректный номер подзадачи.");
                        break;
                }
            }
        }
    }
}