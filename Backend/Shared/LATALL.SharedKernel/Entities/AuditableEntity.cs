
namespace LATALL.SharedKernel.Entities
{
    public abstract class AuditableEntity
    {
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }

    public abstract class MssqlAndMongoAuditableEntity : AuditableEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }

    public abstract class PostgresAuditableEntity : AuditableEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
