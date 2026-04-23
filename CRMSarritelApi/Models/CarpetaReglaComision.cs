using System.Text.Json.Serialization;

namespace CRMSarritelApi.Models
{
    public class CarpetaReglaComision
    {
        public int CarpetaReglasId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual CarpetaReglas? CarpetaReglas { get; set; }

        public int ReglaComisionId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ReglaComision? ReglaComision { get; set; }
    }
}
