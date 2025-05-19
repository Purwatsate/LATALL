using LATALL.SharedKernel.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InventoryService.Domain.Entities
{
    [Table("warehouse", Schema = "dbo")]
    public class Warehouse : PostgresAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Location { get; set; }

        public ICollection<Stock> Stocks { get; set; } = [];
    }

}
