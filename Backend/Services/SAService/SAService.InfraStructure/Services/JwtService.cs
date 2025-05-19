using LATALL.SharedKernel.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SAService.Application.Interfaces;
using SAService.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SAService.InfraStructure.Services
{
    public class JwtService(IOptions<JwtSetting> options, UserManager<AppUser> userManager) : IJwtService
    {
        private readonly JwtSetting _jwtSettings = options.Value;
        private readonly UserManager<AppUser> _userManager = userManager;

        public async Task<string> GenerateToken(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
               {
                   new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                   new(ClaimTypes.Name, $"{user.FirstName}  {user.LastName}"),  
                   // Add additional claims
               };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiryInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
