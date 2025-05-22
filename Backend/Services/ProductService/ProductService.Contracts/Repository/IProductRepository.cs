

using ProductService.Domain.Entities;

namespace ProductService.Contracts.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<List<Product>> GetProductByCategory(string categoryId);
        Task<Product> GetByIdAsync(string id);
        Task CreateAsync(Product product);
        Task UpdateAsync(string id, Product updated);
        Task DeleteAsync(string id);
        Task InsertManyAsync();
    }
}
