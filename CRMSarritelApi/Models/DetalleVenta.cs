using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSarritelApi.Models
{
    public class DetalleVenta : BaseEntity<int>
    {
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public string? DatosConfiguracion { get; set; }

        public int? IdVenta { get; set; }
        public required Venta Venta { get; set; }

        public int? IdProducto { get; set; }
        public required Producto Producto { get; set; }
    }
}
