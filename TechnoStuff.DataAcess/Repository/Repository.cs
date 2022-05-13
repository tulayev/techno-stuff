using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechnoStuff.DataAccess.Data;
using TechnoStuff.DataAccess.Repository.IRepository;

namespace TechnoStuff.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public void Add(T entity) => _dbSet.Add(entity);

        public IEnumerable<T> GetAll(string? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null) 
            {
                foreach (string include in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includes = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);

            if (includes != null)
            {
                foreach (string include in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(T entity) => _dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
    }
}
