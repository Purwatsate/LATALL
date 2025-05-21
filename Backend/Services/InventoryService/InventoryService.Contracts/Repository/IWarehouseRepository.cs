using InventoryService.Domain.Entities;

namespace InventoryService.Contracts.Repository
{
    public interface IWarehouseRepository
    {
        Task<IEnumerable<Warehouse>> GetAllAsync();
        Task<Warehouse?> GetByIdAsync(Guid id);
        Task AddAsync(Warehouse warehouse);
        Task UpdateAsync(Warehouse warehouse);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

    }
}
