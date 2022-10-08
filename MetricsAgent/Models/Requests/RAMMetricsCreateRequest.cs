namespace MetricsAgent.Models.Requests
{
    public class RAMMetricsCreateRequest
    {
        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}
