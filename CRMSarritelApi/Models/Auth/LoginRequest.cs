
namespace CRMSarritelApi.Models.Auth
{
   
    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

   

    public class LoginResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public DateTime AccessTokenExpiry { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public required UserDto User { get; set; }
    }

     

    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Nombre { get; set; }
        public required string Rol { get; set; }
    }

   

    public class RefreshTokenRequest
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
