using TechnoStuff.DataAccess.Data;
using TechnoStuff.DataAccess.Repository.IRepository;
using TechnoStuff.Models;

namespace TechnoStuff.DataAccess.Repository
{
    public class LaptopTypeRepository : Repository<LaptopType>, ILaptopTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public LaptopTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(LaptopType coverType)
        {
            _db.Update(coverType);
        }
    }
}
