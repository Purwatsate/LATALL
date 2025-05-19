
using LATALL.SharedKernel.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Domain.Entities
{
    [Table("order", Schema = "dbo")]
    public class Order : MssqlAndMongoAuditableEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string? Status { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
        public ShippingInfo? ShippingInfo { get; set; }
    }
}
