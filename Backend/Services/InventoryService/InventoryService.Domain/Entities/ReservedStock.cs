using LATALL.SharedKernel.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities
{
    [Table("reserved_stock", Schema = "dbo")]
    public class ReservedStock : PostgresAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? ProductId { get; set; }

        [Required]
        public Guid WarehouseId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public int ReservedQuantity { get; set; }

        public DateTime ReservedOn { get; set; }

        [ForeignKey(nameof(WarehouseId))]
        public Warehouse? Warehouse { get; set; }
    }
}