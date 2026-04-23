using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    public class Cliente : BaseEntity<int>
    {
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Dni { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; } = "PARTICULAR";

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string? Movil { get; set; }

        [StringLength(50)]
        public string? NumeroCuenta { get; set; }

        public bool Penalizado { get; set; } = false;

        public string? NotaPublica { get; set; }
        public string? NotaPrivada { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Value Object embebido (Owned Type)
        public Direccion Direccion { get; set; } = new Direccion("", "", "", "");

        // Relaciones
        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }

    public class Direccion
    {
        // Constructor SIN PARÁMETROS (público, requerido para bindeo de API)
        public Direccion() { }

        // Constructor con parámetros para uso en el dominio
        public Direccion(string calle, string codigoPostal, string poblacion, string provincia)
        {
            Calle = calle;
            CodigoPostal = codigoPostal;
            Poblacion = poblacion;
            Provincia = provincia;
        }

        public string? Calle { get; set; } 
        public string? CodigoPostal { get; set; } 
        public string? Poblacion { get; set; } 
        public string? Provincia { get; set; } 
    }

}
