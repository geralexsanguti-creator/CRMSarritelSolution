namespace CRMSarritelApi.Models
{
    public class Comercial: BaseEntity<int>
    {
        
        public string Nombre { get; set; } = string.Empty;
        public string? Email { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public virtual ICollection<Venta> VentasRealizadas { get; set; } = new List<Venta>();
        public virtual ICollection<Comision> Comisiones { get; set; } = new List<Comision>();
        public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
