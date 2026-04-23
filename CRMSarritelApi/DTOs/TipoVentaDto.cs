using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRMSarritelApi.DTOs
{
    public class TipoVentaDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        // [Required] <-- Debugging: Removed to see what arrives
        [MaxLength(100)]
        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; } = string.Empty;

        [MaxLength(50)]
        [JsonPropertyName("codigo")]
        public string? Codigo { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("activo")]
        public bool Activo { get; set; } = true;

        [JsonPropertyName("esquemaVariablesJson")]
        public string? EsquemaVariablesJson { get; set; }

        [JsonPropertyName("estadosVentaJson")]
        public string? EstadosVentaJson { get; set; }

        [JsonPropertyName("origenesJson")]
        public string? OrigenesJson { get; set; }
    }
}
