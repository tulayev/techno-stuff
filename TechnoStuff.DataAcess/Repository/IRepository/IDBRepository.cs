namespace TechnoStuff.DataAccess.Repository.IRepository
{
    public interface IDBRepository
    {
        ICategoryRepository Category { get; }
        ILaptopTypeRepository LaptopType { get; }
        IProductRepository Product { get; }
        void Save();
    }
}
