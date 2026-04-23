using System;

namespace CRMSarritelApi.Models
{
    public class Comision : BaseEntity<int>
    {
        public int? VentaId { get; set; }
        public int? DetalleVentaId { get; set; }
        public int? ProveedorId { get; set; }
        public int EmpleadoId { get; set; }

        public string? Periodo { get; set; }       // Ej: "2026-02", "2025-12", etc.

        // Reemplazamos TipoId + TipoComision por value object embebido
        public TipoComisionValue Tipo { get; set; } = new TipoComisionValue();

        public decimal? TasaPorcentaje { get; set; }
        public decimal? MontoFijo { get; set; }

        public decimal BaseCalculo { get; set; }
        public decimal MontoComision { get; set; }

        // Reemplazamos EstadoId + EstadoComision por value object embebido
        public EstadoComisionValue Estado { get; set; } = new EstadoComisionValue();

        public string? AppliedReglaNombre { get; set; }
        public string? AppliedTierNombre { get; set; }
        public int? AppliedTierId { get; set; }
        
        public bool EsExtra { get; set; }
        public string? Notas { get; set; }

        public DateTime FechaCalculo { get; set; } = DateTime.UtcNow;
        public DateTime? FechaPago { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual Venta? Venta { get; set; }
        public virtual DetalleVenta? DetalleVenta { get; set; }
        public virtual Proveedor? Proveedor { get; set; }
        public virtual Usuario? Empleado { get; set; }
    }
}
