using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CRMSarritelApi.Models
{
    public class Usuario : BaseEntity<int>
    {
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; } 

        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? FotoPerfil { get; set; }
        
        
        [PasswordPropertyText]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;        

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime FechaCreation { get; set; }

        public bool Activo { get; set; } = true;
        
        [MaxLength(100)]
        public string? Departamento { get; set; }
        
        [MaxLength(100)]
        public string? Puesto { get; set; }
        
        public DateTime? FechaContratacion { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalarioBase { get; set; } 
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Comisiones { get; set; }

        public int? SuperiorId { get; set; }
        [ForeignKey("SuperiorId")]
        public virtual Usuario? Superior { get; set; }

        // Relaciones
        [JsonIgnore]
        public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
        
        [JsonIgnore]
        public virtual ICollection<UsuarioEquipo> UsuarioEquipos { get; set; } = new List<UsuarioEquipo>();
        
        [JsonIgnore]
        public virtual ICollection<Venta> VentasRealizadas { get; set; } = new List<Venta>();
        
        [JsonIgnore]
        public virtual ICollection<Venta> VentasCreadas { get; set; } = new List<Venta>(); // Para los CreadoPorId
        
        [JsonIgnore]
        public virtual ICollection<Comision> ComisionesGeneradas { get; set; } = new List<Comision>();
        
        [JsonIgnore]
        public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

        // Propiedades para Enlace (No mapeadas a BD directamente, usadas en DTO/Controller)
        [NotMapped]
        public int RolId { get; set; }
        
        [NotMapped]
        public List<int>? EquipoIds { get; set; }

        [NotMapped]
        public string? Rol_Nombre { get; set; }
    }
}
