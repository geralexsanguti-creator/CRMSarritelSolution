using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    public class ReglaComision : BaseEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        // Variable a medir (puede ser una de las dinámicas del TipoVenta o una global como 'total_ventas')
        [Required]
        [MaxLength(50)]
        public string Variable { get; set; } = string.Empty;

        // Operador: '>', '<', '>=', '<=', '=', 'between'
        [Required]
        [MaxLength(20)]
        public string Operador { get; set; } = "=";

        // Valores para la comparación. Para 'between' se usan ambos.
        public decimal? ValorMin { get; set; }
        public decimal? ValorMax { get; set; }

        // --- COMISIÓN BRUTA (Gross) ---
        // Valor que el proveedor paga a la empresa por la venta
        [Required]
        [MaxLength(20)]
        public string TipoRemuneracionGross { get; set; } = "fijo";

        public decimal ValorRemuneracionGross { get; set; }
        
        // --- VALOR DE LA VENTA (Referencial Finanzas) ---
        [Required]
        public decimal ValorVenta { get; set; } = 0;

        // --- REPARTICIONES Y CONFIGURACIÓN ---
        public bool Activa { get; set; } = true;
        public int Prioridad { get; set; } = 0;

        // Propiedades de navegación
        public int ProveedorId { get; set; }
        [JsonIgnore]
        public virtual Proveedor? Proveedor { get; set; }

        public int? TipoVentaId { get; set; }
        public virtual TipoVenta? TipoVenta { get; set; }

        public virtual ICollection<ReglaComisionTier> Tiers { get; set; } = new List<ReglaComisionTier>();
        public virtual ICollection<ReparticionComision> ReparticionesBase { get; set; } = new List<ReparticionComision>();
        [JsonIgnore]
        public virtual ICollection<CarpetaReglaComision> CarpetaReglasComision { get; set; } = new List<CarpetaReglaComision>();
    }
}
