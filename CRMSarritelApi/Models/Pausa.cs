using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    public class Pausa : BaseEntity<int>
    {
        [Required]
        public int FichajeId { get; set; }
        
        [ForeignKey("FichajeId")]
        public virtual Fichaje? Fichaje { get; set; }

        [Required]
        public DateTime HoraInicio { get; set; }

        public DateTime? HoraFin { get; set; }

        [MaxLength(200)]
        public string? Motivo { get; set; } = "Descanso";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
