using TechnoStuff.DataAccess.Data;
using TechnoStuff.DataAccess.Repository.IRepository;

namespace TechnoStuff.DataAccess.Repository
{
    public class DBRepository : IDBRepository
    {
        private readonly ApplicationDbContext _db;

        public ICategoryRepository Category { get; private set; }

        public ILaptopTypeRepository LaptopType { get; private set; }

        public IProductRepository Product { get; private set; }

        public DBRepository(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            LaptopType = new LaptopTypeRepository(_db);
            Product = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
