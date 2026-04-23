using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMSarritelApi.DTOs
{
    public class UsuarioUpdateDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(50)]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100)]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El username es obligatorio")]
        [MaxLength(50)]
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string? Password { get; set; } // Opcional en el Update

        [JsonPropertyName("activo")]
        public bool Activo { get; set; }
        
        [JsonPropertyName("departamento")]
        public string? Departamento { get; set; }

        [JsonPropertyName("puesto")]
        public string? Puesto { get; set; }

        [JsonPropertyName("fechaContratacion")]
        public DateTime? FechaContratacion { get; set; }

        [JsonPropertyName("salarioBase")]
        public decimal? SalarioBase { get; set; }

        [JsonPropertyName("comisiones")]
        public decimal? Comisiones { get; set; }

        [JsonPropertyName("rolId")]
        public int RolId { get; set; }

        [JsonPropertyName("equipoIds")]
        public List<int>? EquipoIds { get; set; }

        [JsonPropertyName("fotoPerfil")]
        public string? FotoPerfil { get; set; }
    }
}
