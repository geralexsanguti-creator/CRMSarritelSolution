namespace CRMSarritel.Shared.Models.DTO
{
    public class UsuarioDto
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string? Nombre { get; set; }
        public string? Email { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}