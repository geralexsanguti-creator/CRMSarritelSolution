using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMSarritelApi.Models
{
    public class Permiso : BaseEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty; // e.g., "ventas:edit"

        [MaxLength(200)]
        public string? Descripcion { get; set; }

        [Required]
        [MaxLength(50)]
        public string Modulo { get; set; } = string.Empty; // e.g., "Ventas", "Usuarios"

        // Many-to-many link to Roles via RolPermiso
        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}
