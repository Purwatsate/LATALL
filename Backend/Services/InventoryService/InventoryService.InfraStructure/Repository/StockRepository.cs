using InventoryService.Contracts.Repository;
using InventoryService.Domain.Entities;
using InventoryService.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.InfraStructure.Repository
{
    public class StockRepository(InventoryDbContext context) : IStockRepository
    {
        private readonly InventoryDbContext _context = context;

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(Guid id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task AddAsync(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Stocks.AnyAsync(e => e.Id == id);
        }

        public async Task<Stock?> GetByProductIdAndWarehouseIdAsync(string productId, Guid warehouseId)
        {

            var foundStock = await _context.Stocks.FirstOrDefaultAsync(
                  s => s.ProductId == productId && s.WarehouseId == warehouseId
            );

            if (foundStock != null)
            {
                return foundStock;
            }

            var stockWithoudWarehouseId = await _context.Stocks.FirstOrDefaultAsync(
              s => s.ProductId == productId
            );

            return stockWithoudWarehouseId;
        }
    }
}

