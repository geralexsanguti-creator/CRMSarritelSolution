namespace CRMSarritelClient.Services
{
    public interface IAuthService
    {
        Task Login(string token);
        Task Logout();
    }
}
