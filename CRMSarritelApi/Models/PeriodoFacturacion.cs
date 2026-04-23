using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    public class PeriodoFacturacion : BaseEntity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public bool EsPrincipal { get; set; } = false;

        public bool Abierto { get; set; } = true;

        public DateTime? FechaCierre { get; set; }

        public string? Notas { get; set; }
    }
}
