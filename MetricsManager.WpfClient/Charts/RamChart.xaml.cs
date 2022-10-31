using LiveCharts.Wpf;
using LiveCharts;
using MetricsManager.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MetricsManager.WpfClient
{
    /// <summary>
    /// Логика взаимодействия для RamChart.xaml
    /// </summary>
    public partial class RamChart : UserControl
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private SeriesCollection _columnSeriesValues;
        private RAM_Metrics_Client ramMetricsClient;




        public SeriesCollection ColumnSeriesValues
        {
            get
            {
                return _columnSeriesValues;
            }
            set
            {
                _columnSeriesValues = value;
                OnPropertyChanged("ColumnSeriesValues");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }




        public RamChart()
        {
            InitializeComponent();
            DataContext = this;
        }



        private async void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            if (ramMetricsClient == null)
            {
                AgentsClient agentClient = new AgentsClient("http://localhost:5240", new HttpClient());
                ramMetricsClient = new RAM_Metrics_Client("http://localhost:5240", new HttpClient());

                await agentClient.AgentRegistrationAsync(new AgentInfo
                {
                    AgentAdress = new Uri("http://localhost:5127"),
                    AgentId = 1,
                });
            }

            try
            {

                TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                RAMMetricsResponse response = await ramMetricsClient.RamGetAllByIdAsync(
                    1,
                    fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                    toTime.ToString("dd\\.hh\\:mm\\:ss"));

                if (response.Metrics.Count > 0)
                {

                    PercentDescriptionTextBlock.Text = $"За последние {TimeSpan.FromSeconds(response.Metrics.ToArray()[response.Metrics.Count - 1].Time - response.Metrics.ToArray()[0].Time)} средняя загрузка";

                    PercentTextBlock.Text = $"{response.Metrics.Where(x => x != null).Select(x => x.Value).ToArray().Sum(x => x) / response.Metrics.Count:F2}";
                }

                ColumnSeriesValues = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Values = new ChartValues<float>(response.Metrics.Where(x => x != null).Select(x => (float)x.Value).ToArray())
                    }
                };

                TimePowerChart.Update(true);
            }
            catch (Exception ex)
            {
            }

            TimePowerChart.Update(true);

        }

        private void TimePowerChart_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

