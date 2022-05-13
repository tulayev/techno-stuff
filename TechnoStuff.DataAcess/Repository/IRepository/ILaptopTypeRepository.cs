using TechnoStuff.Models;

namespace TechnoStuff.DataAccess.Repository.IRepository
{
    public interface ILaptopTypeRepository : IRepository<LaptopType>
    {
        void Update(LaptopType laptopType);
    }
}
