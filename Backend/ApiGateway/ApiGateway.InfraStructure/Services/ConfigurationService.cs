using ApiGateway.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ApiGateway.InfraStructure.Services
{
    public class ConfigurationService(IConfiguration configuration) : IConfigurationService
    {
        private readonly IConfiguration _configuration = configuration;

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public string GetApiKey()
        {
            return _configuration["ApiSettings:ApiKey"];
        }
    }
}
