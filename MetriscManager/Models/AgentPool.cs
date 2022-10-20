namespace MetricsManager.Models
{
    public class AgentPool
    {
        private Dictionary<int, AgentInfo> _agents;

        public AgentPool()
        {
            _agents = new Dictionary<int, AgentInfo>();
        }

        public void Add(AgentInfo value)
        {
            if (!_agents.ContainsKey(value.AgentId))
                _agents.Add(value.AgentId, value);
        }
        public AgentInfo[] Get()
        {
            return _agents.Values.ToArray();
        }

        public Dictionary<int, AgentInfo> Agents
        {
            get { return _agents; }
            set { _agents = value; }
        }

    }
}
