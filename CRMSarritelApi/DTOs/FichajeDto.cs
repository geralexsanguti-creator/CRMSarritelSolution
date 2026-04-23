using System;
using System.ComponentModel.DataAnnotations;

namespace CRMSarritelApi.DTOs
{
    public class FichajeDto
    {
        public int Id { get; set; }
        
        [Required]
        public int UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        
        public DateTime HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        
        public string TipoRegistro { get; set; } = "Trabajando";
        public double HorasExtra { get; set; } = 0;
        public string? Notas { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsPausado { get; set; }
        public double TotalPausasMinutos { get; set; }
        public List<PausaDto> Pausas { get; set; } = new List<PausaDto>();
    }

    public class PausaDto
    {
        public int Id { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public string? Motivo { get; set; }
    }

    public class FichajeCreateDto
    {
        [Required]
        public int UsuarioId { get; set; }
        
        public string TipoRegistro { get; set; } = "Trabajando";
        public string? Notas { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
    }

    public class FichajeUpdateDto
    {
        public DateTime? HoraSalida { get; set; }
        public string? TipoRegistro { get; set; }
        public double? HorasExtra { get; set; }
        public string? Notas { get; set; }
    }
}
