using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAService.Domain.Entities
{
    [Table("app_user", Schema = "dbo")]
    public class AppUser : IdentityUser<Guid>
    {
        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
    }
}
