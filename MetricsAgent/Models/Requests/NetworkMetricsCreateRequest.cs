namespace MetricsAgent.Models.Requests
{
    public class NetworkMetricsCreateRequest
    {
        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}
