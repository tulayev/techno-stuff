using TechnoStuff.DataAccess.Data;
using TechnoStuff.DataAccess.Repository.IRepository;
using TechnoStuff.Models;

namespace TechnoStuff.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var dbProduct = _db.Products.FirstOrDefault(p => p.Id == product.Id);

            if (dbProduct != null)
            {
                dbProduct.Title = product.Title;
                dbProduct.Description = product.Description;
                dbProduct.Manufacturer = product.Manufacturer;
                dbProduct.ListPrice = product.ListPrice;
                dbProduct.Price = product.Price;
                dbProduct.Price50 = product.Price50;
                dbProduct.Price100 = product.Price100;
                dbProduct.CategoryId = product.CategoryId;
                dbProduct.LaptopTypeId = product.LaptopTypeId;

                if (product.Image != null)
                {
                    dbProduct.Image = product.Image;
                }
            }
        }
    }
}
