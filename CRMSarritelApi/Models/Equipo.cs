using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMSarritelApi.Models
{
    public class Equipo : BaseEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [MaxLength(255)]
        public string? LogoUrl { get; set; }

        public virtual ICollection<UsuarioEquipo> UsuarioEquipos { get; set; } = new List<UsuarioEquipo>();
    }
}
