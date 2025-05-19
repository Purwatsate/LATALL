using LATALL.SharedKernel.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Domain.Entities
{
    [Table("stock", Schema = "dbo")]
    public class Stock : PostgresAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? ProductId { get; set; }

        [Required]
        public Guid WarehouseId { get; set; }

        public int Quantity { get; set; }

        public DateTime LastUpdated { get; set; }

        [ForeignKey(nameof(WarehouseId))]
        public Warehouse? Warehouse { get; set; }
    }

}
