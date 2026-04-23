using CRMSarritelApi.Models;
using CRMSarritelApi.Models.Auth;
using CRMSarritel.Shared.Models.DTO;

namespace CRMSarritelApi.Services
{
    public interface IAuthService
    {     
            Task<Usuario?> RegisterAsync(RegisterRequest request);
            Task<AuthResponse?> LoginAsync(UsuarioDto request);        
    }
}
