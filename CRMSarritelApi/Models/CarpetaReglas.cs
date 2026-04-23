using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMSarritelApi.Models
{
    public class CarpetaReglas : BaseEntity<int>
    {
        [Required]
        [MaxLength(200)]
        public required string Nombre { get; set; }

        public bool Activo { get; set; } = true;

        public int? ProveedorId { get; set; }
        public virtual Proveedor? Proveedor { get; set; }

        // M:N con ReglaComision
        public virtual ICollection<CarpetaReglaComision> CarpetaReglasComision { get; set; } = new List<CarpetaReglaComision>();

        // M:N con Producto
        public virtual ICollection<ProductoCarpeta> ProductoCarpetas { get; set; } = new List<ProductoCarpeta>();
    }
}
