namespace CRMSarritelApi.DTOs
{
    public class DashboardDto
    {
        public KpisDto Kpis { get; set; } = new();
        public List<RecentSaleDto> RecentSales { get; set; } = new();
        public List<TopSellerDto> TopSellers { get; set; } = new();
    }

    public class KpisDto
    {
        public int TotalClientes { get; set; }
        public int Empresas { get; set; }
        public decimal TotalSalesMonto { get; set; }
        public decimal PendingCommissions { get; set; }
        public int ActiveProducts { get; set; }
        public int TotalProductos { get; set; }
    }

    public class RecentSaleDto
    {
        public int Id { get; set; }
        public string NumeroVenta { get; set; } = "";
        public decimal MontoTotal { get; set; }
        public DateTime FechaVenta { get; set; }
        
        public string ClienteNombre { get; set; } = "";
        public string UsuarioNombre { get; set; } = "";
        
        // Estado (aplanado del embebido)
        public string Estado_Nombre { get; set; } = "";
        public string Estado_Color { get; set; } = "";
        
        // TipoVenta
        public string TipoVenta_Nombre { get; set; } = "";
        public string OrigenVenta { get; set; } = "";
    }

    public class TopSellerDto
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public decimal TotalVendido { get; set; }
        public int Count { get; set; }
    }
}
