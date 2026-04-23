using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;   // ← necesitas este paquete



namespace CRMSarritelClient.Services
{
    

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public CustomAuthStateProvider(
            ILocalStorageService localStorage,
            HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Configuramos el header para todas las peticiones futuras
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public async Task NotifyUserAuthenticationAsync(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task NotifyUserLogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");

            _httpClient.DefaultRequestHeaders.Authorization = null;

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes)!;

            // Manejo especial de roles (puede venir como string o como array JSON)
            if (keyValuePairs.TryGetValue(ClaimTypes.Role, out var rolesObj) && rolesObj != null)
            {
                var rolesJson = rolesObj.ToString()!.Trim();

                if (rolesJson.StartsWith("[") && rolesJson.EndsWith("]"))
                {
                    // Viene como array JSON ["Admin", "User"]
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(rolesJson);
                    if (parsedRoles != null)
                    {
                        foreach (var role in parsedRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }
                }
                else
                {
                    // Viene como string única "Admin" o "User, Moderator"
                    var roles = rolesJson.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
                    }
                }
            }

            // Agregamos el resto de claims (sub, name, email, etc.)
            foreach (var kvp in keyValuePairs)
            {
                if (kvp.Key == ClaimTypes.Role) continue; // ya lo manejamos arriba

                var value = kvp.Value?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    claims.Add(new Claim(kvp.Key, value));
                }
            }

            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
