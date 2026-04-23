using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSarritelApi.Models
{
    public class Producto : BaseEntity<int>
    {
        [MaxLength(50)]
        public required string Nombre { get; set; }
        [MaxLength(1000)]
        public required string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioOferta { get; set; }
        public int? Cantidad { get; set; }
        public bool EsInfinito { get; set; } = false;
        public DateTime? FechaLimite { get; set; }
        public required string Imagen { get; set; }
        public DateTime? FechaCreation { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime? FechaActivacion { get; set; }
        public DateTime? FechaBaja { get; set; }

        public int? TipoVentaId { get; set; }
        public virtual TipoVenta? TipoVenta { get; set; }

        public int? ProveedorId { get; set; }
        public virtual Proveedor? Proveedor { get; set; }

        public virtual ICollection<ProductoCarpeta> ProductoCarpetas { get; set; } = new List<ProductoCarpeta>();

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<int>? CarpetaIds { get; set; }
    }
}

