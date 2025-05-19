using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.InfraStructure.Data
{
    public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<ShippingInfo> ShippingInfos => Set<ShippingInfo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Items)
                      .WithOne()
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.ShippingInfo)
                      .WithOne()
                      .HasForeignKey<ShippingInfo>(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ShippingInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
