using System;
using System.Collections.Generic;

namespace CRMSarritelApi.DTOs
{
    public class SaleCommissionSummaryDto
    {
        public int VentaId { get; set; }
        public int? DetalleVentaId { get; set; }
        public string NumeroVenta { get; set; } = string.Empty;
        public decimal BaseBruta { get; set; }
        public decimal TotalComisionado { get; set; }
        public decimal Remanente { get; set; }
        
        public List<BeneficiarySummaryDto> Beneficiarios { get; set; } = new();
    }

    public class BeneficiarySummaryDto
    {
        public int Id { get; set; }
        public int? DetalleVentaId { get; set; }
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
