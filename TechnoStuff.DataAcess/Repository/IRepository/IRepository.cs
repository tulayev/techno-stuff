using System.Linq.Expressions;

namespace TechnoStuff.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includes = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includes = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
