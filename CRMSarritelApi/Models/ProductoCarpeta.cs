using System.Text.Json.Serialization;

namespace CRMSarritelApi.Models
{
    public class ProductoCarpeta
    {
        public int ProductoId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Producto? Producto { get; set; }

        public int CarpetaReglasId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual CarpetaReglas? CarpetaReglas { get; set; }
    }
}
