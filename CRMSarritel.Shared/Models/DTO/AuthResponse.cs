using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSarritel.Shared.Models.DTO
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
        public bool CanViewAllCommissions { get; set; }
        public bool IsManager { get; set; }
        public List<int> ManagedEquipoIds { get; set; } = new();
        public required string Token { get; set; }
    }
}
