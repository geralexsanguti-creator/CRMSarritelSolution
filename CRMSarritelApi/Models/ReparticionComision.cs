using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    public class ReparticionComision : BaseEntity<int>
    {
        public int? RolId { get; set; }
        public virtual Rol? Rol { get; set; }

        public int? EquipoId { get; set; }
        public virtual Equipo? Equipo { get; set; }

        public int? UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }

        // Tipo de cálculo: 'fijo' o 'porcentaje'
        [Required]
        [MaxLength(20)]
        public string TipoCalculo { get; set; } = "porcentaje";

        // Valor a repartir (ej: 0.10 para 10%)
        public decimal Valor { get; set; }

        public int? ReglaComisionId { get; set; }
        [JsonIgnore]
        public virtual ReglaComision? ReglaComision { get; set; }

        public int? TierId { get; set; }
        [JsonIgnore]
        public virtual ReglaComisionTier? Tier { get; set; }
    }
}
