using System.Text.Json.Serialization;

namespace CRMSarritelApi.DTOs
{
    public class VentaDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("numeroVenta")]
        public string NumeroVenta { get; set; } = "";
        
        // Finances
        [JsonPropertyName("montoVenta")]
        public decimal MontoVenta { get; set; }

        [JsonPropertyName("descuentoPorcentaje")]
        public decimal DescuentoPorcentaje { get; set; }

        [JsonPropertyName("descuentoMonto")]
        public decimal DescuentoMonto { get; set; }

        [JsonPropertyName("montoTotal")]
        public decimal MontoTotal { get; set; }
        
        // Dates & Notes
        [JsonPropertyName("fechaVenta")]
        public DateTime FechaVenta { get; set; }

        [JsonPropertyName("fechaInstalacionPrevista")]
        public DateTime? FechaInstalacionPrevista { get; set; }

        [JsonPropertyName("fechaInstalacionReal")]
        public DateTime? FechaInstalacionReal { get; set; }

        [JsonPropertyName("notas")]
        public string? Notas { get; set; }

        [JsonPropertyName("clienteId")]
        public int ClienteId { get; set; }

        [JsonPropertyName("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonPropertyName("clienteNombre")]
        public string ClienteNombre { get; set; } = "";

        [JsonPropertyName("usuarioNombre")]
        public string UsuarioNombre { get; set; } = "";
        
        [JsonPropertyName("estado_Codigo")]
        public string Estado_Codigo { get; set; } = "";

        [JsonPropertyName("estado_Nombre")]
        public string Estado_Nombre { get; set; } = "";

        [JsonPropertyName("estado_Color")]
        public string Estado_Color { get; set; } = "";

        [JsonPropertyName("estado_Icono")]
        public string Estado_Icono { get; set; } = "";

        [JsonPropertyName("estado_EsGanada")]
        public bool Estado_EsGanada { get; set; }

        [JsonPropertyName("estado_EsFinal")]
        public bool Estado_EsFinal { get; set; }
        
        [JsonPropertyName("tipoVentaId")]
        public int? TipoVentaId { get; set; }

        [JsonPropertyName("tipoVenta_Codigo")]
        public string TipoVenta_Codigo { get; set; } = "";

        [JsonPropertyName("tipoVenta_Nombre")]
        public string TipoVenta_Nombre { get; set; } = "";

        [JsonPropertyName("tipoVenta_Descripcion")]
        public string TipoVenta_Descripcion { get; set; } = "";

        [JsonPropertyName("origenVenta")]
        public string OrigenVenta { get; set; } = "";

        [JsonPropertyName("detalles")]
        public List<DetalleVentaDto> Detalles { get; set; } = new();
    }

    public class DetalleVentaDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("idProducto")]
        public int IdProducto { get; set; }

        [JsonPropertyName("productoNombre")]
        public string ProductoNombre { get; set; } = "";

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("datosConfiguracion")]
        public string? DatosConfiguracion { get; set; }
    }
}
