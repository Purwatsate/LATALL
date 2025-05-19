
using InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.InfraStructure.Data
{
    public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
    {
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<ReservedStock> ReservedStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .HasIndex(s => new { s.ProductId, s.WarehouseId })
                .IsUnique();

            modelBuilder.Entity<ReservedStock>()
                .HasIndex(r => new { r.ProductId, r.WarehouseId, r.OrderId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }

}
