using CRMSarritel.Shared.Models.DTO;
using CRMSarritelApi.Models;
using CRMSarritelApi.Models.Auth;
using CRMSarritelApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRMSarritelApi.Services
{
    public class AuthService(IUsuarioRepository _userrepo, IConfiguration config) : IAuthService
    {
        
        private readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();

        /*
        public async Task<string?> LoginAsync(UsuarioDto request)
        {
            // Buscar usuario (usa el método específico si lo tienes)
            var user = await _userrepo.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                return null;
            }

            // Verificar contraseña (request.Password es texto plano)
            var verificationResult = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,          // hash almacenado
                request.Password            // contraseña ingresada por el usuario
            );

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            // Opcional: si es SuccessRehashNeeded → puedes re-hashear aquí
            if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
                _userrepo.Actualizar(user);
                await _userrepo.SaveChangesAsync();
            }

            return CreateToken(user);
        }
        */
        public async Task<AuthResponse?> LoginAsync(UsuarioDto request)
        {
            var username = (request?.Username ?? "").Trim();
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(request?.Password))
                return null;

            var user = await _userrepo.GetByUsernameWithRolesAsync(username);
            if (user == null) return null;

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, request.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                // Fallback a BCrypt
                try
                {
                    if (BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
                        _userrepo.Actualizar(user);
                        await _userrepo.SaveChangesAsync();
                        verificationResult = PasswordVerificationResult.Success;
                    }
                    else return null;
                }
                catch { return null; }
            }

            if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
                _userrepo.Actualizar(user);
                await _userrepo.SaveChangesAsync();
            }

            var roles = user.UsuarioRoles?.Select(ur => ur.Rol?.Nombre).OfType<string>().Distinct(StringComparer.OrdinalIgnoreCase).ToList() ?? new List<string>();
            var permissions = user.UsuarioRoles?
                .SelectMany(ur => ur.Rol?.RolPermisos ?? new List<RolPermiso>())
                .Select(rp => rp.Permiso?.Nombre)
                .OfType<string>()
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList() ?? new List<string>();

            var canViewAll = user.UsuarioRoles?.Any(ur => ur.Rol?.CanViewAllCommissions == true) ?? false;
            var managedTeamIds = user.UsuarioEquipos?.Where(ue => ue.EsManager).Select(ue => ue.EquipoId).ToList() ?? new List<int>();
            var isManager = managedTeamIds.Any();
            var token = CreateToken(user, roles, canViewAll);

            return new AuthResponse
            {
                Id = user.Id,
                Username = user.Username,
                Nombre = user.Nombre ?? user.Username,
                Email = user.Email,
                Roles = roles,
                Permissions = permissions,
                CanViewAllCommissions = canViewAll,
                IsManager = isManager,
                ManagedEquipoIds = managedTeamIds,
                Token = token
            };
        }

        public async Task<Usuario?> RegisterAsync(RegisterRequest request)
        {
            if (await _userrepo.AnyAsync(u => u.Username == request.Username))
                return null;

            var user = new Usuario
            {
                Username = request.Username,
                Nombre = request.Nombre ?? request.Username,
                Email = request.Email,
                FechaCreation = DateTime.UtcNow
            };

            // Hashear contraseña
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            // Asignar rol
            var usuarioRol = new UsuarioRol
            {
                Usuario = user,
                RolId = request.RolId,
                FechaAsignacion = DateTime.UtcNow
            };

            user.UsuarioRoles = new List<UsuarioRol> { usuarioRol };

            await _userrepo.Insertar(user);
            await _userrepo.SaveChangesAsync();

            return user;
        }

        private string CreateToken(Usuario user, IReadOnlyList<string> roles, bool canViewAll)
        {
            var isManager = user.UsuarioEquipos?.Any(ue => ue.EsManager) ?? false;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("can_view_all_commissions", canViewAll.ToString().ToLower()),
                new Claim("is_manager", isManager.ToString().ToLower()),
            };

            // Add all roles
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config.GetValue<string>("AppSettings:Token")
                    ?? throw new InvalidOperationException("JWT secret is missing")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: config.GetValue<string>("AppSettings:Issuer"),
                audience: config.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),           // consider shorter + refresh token
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
