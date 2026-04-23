using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    public class Fichaje : BaseEntity<int>
    {
        [Required]
        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }

        [Required]
        public DateTime HoraEntrada { get; set; }
        
        public DateTime? HoraSalida { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipoRegistro { get; set; } = "Trabajando";

        // Vacaciones, Baja medica, Permiso, Festivo

        public double HorasExtra { get; set; } = 0;

        [MaxLength(1000)]
        public string? Notas { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Pausa> Pausas { get; set; } = new List<Pausa>();
    }
}
