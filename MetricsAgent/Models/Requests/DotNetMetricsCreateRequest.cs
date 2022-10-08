namespace MetricsAgent.Models.Requests
{
    public class DotNetMetricsCreateRequest
    {
        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}
