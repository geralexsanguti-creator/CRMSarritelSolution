using CRMSarritelClient.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CRMSarritelClient.Services
{
    public class AuthService(CustomAuthProvider customAuthProvider)
    {
        private readonly CustomAuthProvider _customAuthProvider = customAuthProvider;

        public async Task<string?> GetUserNameAsync()
        {
            var authState = await _customAuthProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst("unique_name")?.Value;
        }

        public async Task<string?> GetUserRoleAsync()
        {
            var authState = await _customAuthProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(ClaimTypes.Role)?.Value;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var authState = await _customAuthProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Identity!.IsAuthenticated;
        }

        public async Task<string?> GetNameAsync()
        {
            var authState = await _customAuthProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst("Nombre")?.Value;
        }


    }
}
