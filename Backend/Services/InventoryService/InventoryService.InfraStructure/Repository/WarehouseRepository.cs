using InventoryService.Contracts.Repository;
using InventoryService.Domain.Entities;
using InventoryService.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.InfraStructure.Repository
{

    public class WarehouseRepository(InventoryDbContext context) : IWarehouseRepository
    {
        private readonly InventoryDbContext _context = context;

        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            return await _context.Warehouses
                    .Where(w => w.IsActive)
                    .ToListAsync();
        }

        public async Task<Warehouse?> GetByIdAsync(Guid id)
        {
            return await _context.Warehouses.FindAsync(id);
        }

        public async Task AddAsync(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Warehouse warehouse)
        {
            _context.Entry(warehouse).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse != null)
            {
                _context.Warehouses.Remove(warehouse);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Warehouses.AnyAsync(e => e.Id == id);
        }
    }
}
