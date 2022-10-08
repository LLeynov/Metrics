namespace MetricsManager.Models
{
    public class AgentPool
    {
        private Dictionary<int, AgentInfo> _agents;

        public AgentPool()
        {
            _agents = new Dictionary<int, AgentInfo>();
        }

        public void Add(AgentInfo agent)
        {
            lock (_agents)
            {
                if (!_agents.ContainsKey(agent.AgentId))
                    _agents.Add(agent.AgentId, agent);
            }
        }

        public AgentInfo[] Get()
        {
            return _agents.Values.ToArray();
        }

        public Dictionary<int, AgentInfo> Agent
        {
            get { return _agents; }
            set { _agents = value; }
        }
    }
}
