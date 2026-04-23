using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    [Table("ProductoReglas")]
    public class ProductoRegla
    {
        public int ProductoId { get; set; }
        public virtual Producto? Producto { get; set; }

        public int ReglaComisionId { get; set; }
        public virtual ReglaComision? ReglaComision { get; set; }

        public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;
    }
}
