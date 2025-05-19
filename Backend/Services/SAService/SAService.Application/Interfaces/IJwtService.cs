using SAService.Domain.Entities;

namespace SAService.Application.Interfaces
{
    public interface IJwtService
    {
        public Task<string> GenerateToken(AppUser user);
    }
}
