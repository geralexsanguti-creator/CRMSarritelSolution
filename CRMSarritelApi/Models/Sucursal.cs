using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSarritelApi.Models
{
    public class Sucursal: BaseEntity<int>
    {
        public required string Nombre { get; set; }
    }
}
