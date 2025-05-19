using LATALL.SharedKernel.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService.Domain.Entities
{
    public class Product : MssqlAndMongoAuditableEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? Sku { get; set; } //Stock Keeping Unit
        public string? CategoryId { get; set; }
    }
}
