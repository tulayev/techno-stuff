using Microsoft.EntityFrameworkCore;
using TechnoStuff.Models;

namespace TechnoStuff.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<LaptopType> LaptopTypes { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
