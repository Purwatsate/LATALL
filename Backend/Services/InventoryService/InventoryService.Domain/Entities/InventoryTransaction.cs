using LATALL.SharedKernel.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities
{
    [Table("inventory_transaction", Schema = "dbo")]
    public class InventoryTransaction : PostgresAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? ProductId { get; set; }

        [Required]
        public Guid WarehouseId { get; set; }

        [Required]
        public string? ChangeType { get; set; }

        public int Quantity { get; set; }

        public string? Reason { get; set; }

        [ForeignKey(nameof(WarehouseId))]
        public Warehouse? Warehouse { get; set; }
    }
}
