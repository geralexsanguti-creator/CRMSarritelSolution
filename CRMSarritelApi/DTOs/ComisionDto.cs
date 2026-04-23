using System.Text.Json.Serialization;

namespace CRMSarritelApi.DTOs
{
    public class ComisionDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ventaId")]
        public int? VentaId { get; set; }

        [JsonPropertyName("venta_Numero")]
        public string? Venta_Numero { get; set; }

        [JsonPropertyName("detalleVentaId")]
        public int? DetalleVentaId { get; set; }

        [JsonPropertyName("empleadoId")]
        public int EmpleadoId { get; set; }

        [JsonPropertyName("empleadoNombre")]
        public string? EmpleadoNombre { get; set; }

        [JsonPropertyName("periodo")]
        public string? Periodo { get; set; }

        [JsonPropertyName("tipo_Nombre")]
        public string? Tipo_Nombre { get; set; }

        [JsonPropertyName("montoComision")]
        public decimal MontoComision { get; set; }

        [JsonPropertyName("baseCalculo")]
        public decimal BaseCalculo { get; set; }

        [JsonPropertyName("tasaPorcentaje")]
        public decimal? TasaPorcentaje { get; set; }

        [JsonPropertyName("montoFijo")]
        public decimal? MontoFijo { get; set; }

        [JsonPropertyName("estado_Codigo")]
        public string? Estado_Codigo { get; set; }

        [JsonPropertyName("estado_Nombre")]
        public string? Estado_Nombre { get; set; }

        [JsonPropertyName("estado_Color")]
        public string? Estado_Color { get; set; }

        [JsonPropertyName("estado_Icono")]
        public string? Estado_Icono { get; set; }

        [JsonPropertyName("estado_EsPagable")]
        public bool Estado_EsPagable { get; set; }

        [JsonPropertyName("fechaCalculo")]
        public DateTime FechaCalculo { get; set; }

        [JsonPropertyName("fechaPago")]
        public DateTime? FechaPago { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("vendedorId")]
        public int? VendedorId { get; set; }

        [JsonPropertyName("vendedorNombre")]
        public string? VendedorNombre { get; set; }

        [JsonPropertyName("productoNombre")]
        public string? ProductoNombre { get; set; }

        [JsonPropertyName("productoIcono")]
        public string? ProductoIcono { get; set; }

        [JsonPropertyName("proveedorId")]
        public int? ProveedorId { get; set; }

        [JsonPropertyName("proveedorNombre")]
        public string? ProveedorNombre { get; set; }

        [JsonPropertyName("appliedReglaNombre")]
        public string? AppliedReglaNombre { get; set; }

        [JsonPropertyName("appliedTierNombre")]
        public string? AppliedTierNombre { get; set; }

        [JsonPropertyName("appliedTierId")]
        public int? AppliedTierId { get; set; }

        [JsonPropertyName("notas")]
        public string? Notas { get; set; }

        [JsonPropertyName("esExtra")]
        public bool EsExtra { get; set; }
    }
}
