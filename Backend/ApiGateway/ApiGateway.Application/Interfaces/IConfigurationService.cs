namespace ApiGateway.Application.Interfaces
{
    public interface IConfigurationService
    {
            string GetConnectionString();
            string GetApiKey();
       
    }
}
