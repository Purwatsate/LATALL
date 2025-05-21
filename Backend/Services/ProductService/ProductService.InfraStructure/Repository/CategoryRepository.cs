
using MongoDB.Driver;
using ProductService.Contracts.Repository;
using ProductService.Domain.Entities;

namespace ProductService.InfraStructure.Repository
{
    public class CategoryRepository(IMongoDatabase db) : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categoryCollection = db.GetCollection<Category>("Categories");
        List<Category> categories =
        [
    new() {
        Name = "Electronics",
        ParentId = null,
        IsActive = true
    },
    new Category
    {
        Name = "Mobile Phones",
        ParentId = null,
        IsActive = true
    },
    new Category
    {
        Name = "Laptops",
        ParentId = null,
        IsActive = true
    },
    new Category
    {
        Name = "Accessories",
        ParentId = null,
        IsActive = true
    },
    new Category
    {
        Name = "Home Appliances",
        ParentId = null,
        IsActive = true
    }

];
        public async Task InsertManyAsync() =>
        await _categoryCollection.InsertManyAsync(categories);
    }
}
