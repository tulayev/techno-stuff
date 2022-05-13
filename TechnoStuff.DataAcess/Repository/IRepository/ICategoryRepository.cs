using TechnoStuff.Models;

namespace TechnoStuff.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
