using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();

        T GetById(int id);

        void Create(T item);
    }
}
