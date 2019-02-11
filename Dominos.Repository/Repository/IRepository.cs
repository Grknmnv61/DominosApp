using System.Linq;

namespace Dominos.Repository.Repository
{
    public interface IRepository<R>
    {
        R Context { get; set; }
        IQueryable<T> Query<T>() where T : class;
        int Commit();
        void RollBack();
    }
}
