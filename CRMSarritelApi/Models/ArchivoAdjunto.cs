using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSarritelApi.Models
{
    public class ArchivoAdjunto : BaseEntity<int>
    {
        // Polymorphic reference (e.g. "Cliente", "Venta", "Usuario", "Producto")
        [Required]
        [StringLength(50)]
        public string EntidadTipo { get; set; } = string.Empty;

        // The ID of the primary key in the target table
        [Required]
        public int EntidadId { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreArchivo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string TipoMime { get; set; } = string.Empty;

        public long TamanoBytes { get; set; } = 0;

        // Binary representation in Postgres (bytea)
        // Set ignore for Json serialization if needed to avoid massive JSONs
        [Column(TypeName = "bytea")]
        public byte[] ArchivoBytes { get; set; } = Array.Empty<byte>();

        [StringLength(500)]
        public string? Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public int? CreadoPorId { get; set; }

        public virtual Usuario? CreadoPor { get; set; }
    }
}
