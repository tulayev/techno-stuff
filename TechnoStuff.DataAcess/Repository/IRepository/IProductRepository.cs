﻿using TechnoStuff.Models;

namespace TechnoStuff.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
