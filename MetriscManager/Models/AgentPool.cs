namespace MetricsManager.Models
{
    public class AgentPool
    {
        //private static AgentPool _instance;

        //public static AgentPool Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new AgentPool();
        //        return _instance;
        //    }
        //}

        private Dictionary<int, AgentInfo> _agents;

        public AgentPool()
        {
            _agents = new Dictionary<int, AgentInfo>();
        }

        public void Add(AgentInfo agent)
        {
            if (!_agents.ContainsKey(agent.AgentId))
                _agents.Add(agent.AgentId, agent);
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
