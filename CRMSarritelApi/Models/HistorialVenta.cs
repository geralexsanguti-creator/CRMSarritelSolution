using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    public class HistorialVenta : BaseEntity<int>
    {
        public int VentaId { get; set; }
        public virtual Venta? Venta { get; set; }

        public int? UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }

        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        public string Accion { get; set; } = string.Empty; // e.g. "Estado", "Datos", "Finanzas"
        public string Descripcion { get; set; } = string.Empty; // e.g. "Estado cambiado de Revisando a Activo"
        
        public string? CampoModificado { get; set; }
        public string? ValorAnterior { get; set; }
        public string? ValorNuevo { get; set; }
    }
}
