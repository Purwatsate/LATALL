
using InventoryService.Domain.Entities;

namespace InventoryService.Contracts.Repository
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(Guid id);
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<Stock?> GetByProductIdAndWarehouseIdAsync(string productId, Guid? warehouseId);
    }
}
