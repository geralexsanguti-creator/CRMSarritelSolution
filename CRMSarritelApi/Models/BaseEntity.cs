using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSarritelApi.Models
{
    public abstract class BaseEntity<Tid>
    {
        public Tid Id { get; set; }= default!;
        
    }
}
