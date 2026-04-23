using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMSarritelApi.Models
{
    public class TipoVenta : BaseEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Codigo { get; set; }

        public string? Descripcion { get; set; }

        public bool Activo { get; set; } = true;

        // Definición de variables dinámicas en formato JSON (ej: [{"nombre": "lineas", "tipo": "number"}])
        public string? EsquemaVariablesJson { get; set; }

        // Definición de estados del pipeline en formato JSON
        public string? EstadosVentaJson { get; set; }

        // Definición de orígenes de venta válidos (ej: ["presencial", "web", "referido"])
        public string? OrigenesJson { get; set; }

        [JsonIgnore]
        public virtual ICollection<Proveedor> Proveedores { get; set; } = new List<Proveedor>();
    }
}
