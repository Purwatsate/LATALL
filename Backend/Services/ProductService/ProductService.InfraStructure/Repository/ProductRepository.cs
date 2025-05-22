using MongoDB.Driver;
using ProductService.Contracts.Repository;
using ProductService.Domain.Entities;

namespace ProductService.InfraStructure.Repository
{
    public class ProductRepository(IMongoDatabase db) : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection = db.GetCollection<Product>("Products");

        public async Task<List<Product>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return await _collection
                .Find(_ => true)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }


        public async Task<List<Product>> GetProductByCategory(string categoryId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id) =>
            await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
            await _collection.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product updated) =>
            await _collection.ReplaceOneAsync(p => p.Id == id, updated);

        public async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(p => p.Id == id);

        public async Task InsertManyAsync()
        {
            var products = new List<Product>
            {
                new()
                {
                    Name = "iPhone 15 Pro",
                    Description = "Apple smartphone with A17 chip",
                    Price = 1199.99m,
                    DiscountPrice = 1099.99m,
                    Sku = "IPH15PRO",
                    CategoryId = null, // fill this with actual Category ObjectId later
                    IsActive = true,
                    CreatedBy = "B0BE0342-FF28-42AB-A4F1-8CB0C9266C47"
                },
                new()
                {
                    Name = "Samsung Galaxy S24",
                    Description = "Flagship Android phone",
                    Price = 999.99m,
                    DiscountPrice = 899.99m,
                    Sku = "SGS24",
                    CategoryId = null,
                    IsActive = true,
                    CreatedBy = "B0BE0342-FF28-42AB-A4F1-8CB0C9266C47"
                },
                new()
                {
                    Name = "Dell XPS 15",
                    Description = "High-performance laptop",
                    Price = 1499.99m,
                    DiscountPrice = 1299.99m,
                    Sku = "DXPS15",
                    CategoryId = null,
                    IsActive = true,
                    CreatedBy = "adB0BE0342-FF28-42AB-A4F1-8CB0C9266C47min"
                },
                new()
                {
                    Name = "Sony WH-1000XM5",
                    Description = "Noise-cancelling headphones",
                    Price = 399.99m,
                    DiscountPrice = 349.99m,
                    Sku = "SONYXM5",
                    CategoryId = null,
                    IsActive = true,
                    CreatedBy = "B0BE0342-FF28-42AB-A4F1-8CB0C9266C47"
                },
                new()
                {
                    Name = "LG Refrigerator",
                    Description = "Smart inverter fridge",
                    Price = 899.99m,
                    DiscountPrice = 799.99m,
                    Sku = "LGFRIDGE",
                    CategoryId = null,
                    IsActive = true,
                    CreatedBy = "B0BE0342-FF28-42AB-A4F1-8CB0C9266C47"
                }
            };

            await _collection.InsertManyAsync(products);
        }
    }

}
