using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;


namespace ProductService.InfraStructure.Persistence
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
