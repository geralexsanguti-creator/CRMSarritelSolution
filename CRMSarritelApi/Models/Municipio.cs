namespace CRMSarritelApi.Models
{
    public class Municipio : BaseEntity<int>
    {
        public string Nombre { get; set; } = string.Empty;        
        public string? CodigoPostal { get; set; }
        public bool Activo { get; set; } = true;

         //Relaciones
        public int IdProvincia { get; set; }
        public virtual Provincia? Provincia { get; set; }
        
    }
}
