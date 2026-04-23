namespace CRMSarritelApi.Models
{
    public class Provincia : BaseEntity<int>
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Codigo { get; set; }        

        // Relaciones
        public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
    }
}
