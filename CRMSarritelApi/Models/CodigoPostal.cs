namespace CRMSarritelApi.Models
{
    public class CodigoPostal : BaseEntity<int>
    {
        public string Codigo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Localidad { get; set; } = string.Empty;

        // Relaciones
        public int ProvinciaId { get; set; }
        public int MunicipioId { get; set; }

        public virtual Provincia? Provincia { get; set; }
        public virtual Municipio? Municipio { get; set; }
    }
}

