namespace MetricsAgent.Models.Requests
{
    public class HDDMetricsCreateRequest
    {
        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}
