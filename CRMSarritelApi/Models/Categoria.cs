using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSarritelApi.Models
{
    public class Categoria : BaseEntity<int>
    {
        [MaxLength(50)]
        public string? Nombre { get; set; }
        public DateTime? FechaCreation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    }
}
