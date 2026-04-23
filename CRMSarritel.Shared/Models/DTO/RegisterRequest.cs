using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSarritel.Shared.Models.DTO
{
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string? Nombre { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RolId { get; set; }  // Para recibir el ID del rol seleccionado
    }
}
