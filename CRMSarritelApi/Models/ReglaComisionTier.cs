using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    public class ReglaComisionTier : BaseEntity<int>
    {
        public int ReglaComisionId { get; set; }
        [JsonIgnore]
        public virtual ReglaComision? ReglaComision { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = "Nuevo Tier";

        // Rango de activación basado en la variable de la regla (ej: total_ventas)
        public decimal ValorMin { get; set; }
        public decimal? ValorMax { get; set; }

        // Sobrescritura de la comisión bruta (Gross) para este tier
        [MaxLength(20)]
        public string? TipoRemuneracionGross { get; set; } // 'fijo' | 'porcentaje'
        public decimal? ValorRemuneracionGross { get; set; }

        // Reparticiones específicas cuando este tier está activo
        public virtual ICollection<ReparticionComision> Reparticiones { get; set; } = new List<ReparticionComision>();
    }
}
