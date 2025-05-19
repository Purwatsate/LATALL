using LATALL.SharedKernel.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService.Domain.Entities
{
    public class Category : MssqlAndMongoAuditableEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ParentId { get; set; }
        public bool IsActive { get; set; }
    }
}
