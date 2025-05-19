using Microsoft.AspNetCore.Identity;
using SAService.API.Controller;
using SAService.API.Manager.Interfaces;

namespace SAService.API.Manager
{
    public class SystemLoginManager(RoleManager<IdentityRole<Guid>> roleManager, ILogger<SystemLoginController> logger) : ISystemLoginManager
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
        private readonly ILogger<SystemLoginController> _logger = logger;
        public async Task<bool> CreateRole(string role)
        {
            try
            {
                _logger.LogInformation("CreateRole Mgr Started");
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    var createRoleResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    if (!createRoleResult.Succeeded)
                    {
                        foreach (var error in createRoleResult.Errors)
                        {
                            _logger.LogError("Error creating role: {ErrorDescription}", error.Description);
                        }
                        return false;
                    }
                }
                _logger.LogInformation("CreateRole Mgr Finished");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
