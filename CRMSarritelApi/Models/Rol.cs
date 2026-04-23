using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CRMSarritelApi.Models
{
    public class Rol : BaseEntity<int>
    {
        public required string Nombre { get; set; } = string.Empty;

        // Many-to-many with Users
        public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();

        // Link to Permissions (Many-to-Many via RolPermiso)
        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();

        [JsonIgnore]
        public virtual ICollection<ReparticionComision> Reparticiones { get; set; } = new List<ReparticionComision>();

        public bool CanViewAllCommissions { get; set; } = false;
    }
}
