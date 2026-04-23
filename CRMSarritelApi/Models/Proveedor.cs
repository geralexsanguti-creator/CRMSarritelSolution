using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMSarritelApi.Models
{
    public class Proveedor : BaseEntity<int>
    {
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? NombrePlataforma { get; set; }

        [MaxLength(20)]
        public string? CIF { get; set; }

        [MaxLength(255)]
        public string? Web { get; set; }

        [MaxLength(150)]
        public string? EmailContacto { get; set; }

        [MaxLength(50)]
        public string? Telefono { get; set; }

        public string? LogoUrl { get; set; }

        public bool Activo { get; set; } = true;

        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
        public virtual ICollection<ReglaComision> Reglas { get; set; } = new List<ReglaComision>();
        
        // Relación con los tipos de venta que opera este proveedor
        public virtual ICollection<TipoVenta> TiposVenta { get; set; } = new List<TipoVenta>();

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public List<int>? TipoVentaIds { get; set; }
    }
}
