using System;
using System.Text.Json.Serialization;

namespace CRMSarritelApi.Models
{
    public class UsuarioEquipo
    {
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;

        public int EquipoId { get; set; }
        
        [JsonIgnore]
        public virtual Equipo Equipo { get; set; } = null!;

        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;
        public bool EsManager { get; set; } = false;
    }
}
