namespace SAService.API.Manager.Interfaces
{
    public interface ISystemLoginManager
    {
        public Task<bool> CreateRole(string role);
    }
}
