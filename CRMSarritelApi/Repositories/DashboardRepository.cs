using CRMSarritelApi.Data;
using CRMSarritelApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSarritelApi.Repositories
{
    public class DashboardRepository(CRMSarritelDbContext context) : IDashboardRepository
    {
        private readonly CRMSarritelDbContext _context = context;

        public async Task<DashboardDto> GetDashboardMetricsAsync(int userId)
        {
            var dto = new DashboardDto();

            // 1. Evaluate permissions
            bool hasViewAll = await _context.UsuarioRoles
                .Where(ur => ur.UsuarioId == userId)
                .AnyAsync(ur => ur.Rol!.Nombre == "Admin" || 
                               ur.Rol.Nombre == "SuperAdmin" || 
                               ur.Rol.RolPermisos.Any(rp => rp.Permiso.Nombre == "dashboard:view_all" || rp.Permiso.Nombre == "ventas:view_all"));

            // 2. Fetch scoped users if restricted
            List<int> visibleUserIds = new List<int> { userId };
            if (!hasViewAll)
            {
                // Solo incluimos miembros si el usuario es manager del equipo
                var managedTeamIds = await _context.UsuarioEquipos
                    .Where(ue => ue.UsuarioId == userId && ue.EsManager)
                    .Select(ue => ue.EquipoId)
                    .ToListAsync();
                
                if (managedTeamIds.Any())
                {
                    var teamUsers = await _context.UsuarioEquipos
                        .Where(ue => managedTeamIds.Contains(ue.EquipoId))
                        .Select(ue => ue.UsuarioId)
                        .ToListAsync();
                    
                    visibleUserIds.AddRange(teamUsers);
                    visibleUserIds = visibleUserIds.Distinct().ToList();
                }
            }

            // 3. IQueryables for filtering
            var clientesQuery = _context.Clientes.AsQueryable();
            var ventasQuery = _context.Ventas.AsQueryable();
            var comisionesQuery = _context.Comisiones.AsQueryable();

            if (!hasViewAll)
            {
                ventasQuery = ventasQuery.Where(v => visibleUserIds.Contains(v.UsuarioId));
                comisionesQuery = comisionesQuery.Where(c => visibleUserIds.Contains(c.EmpleadoId));
            }

            // 4. Calculate KPIs
            dto.Kpis.TotalClientes = await _context.Clientes.CountAsync();
            dto.Kpis.Empresas = await _context.Clientes.CountAsync(c => c.Tipo == "EMPRESA");
            dto.Kpis.TotalSalesMonto = await ventasQuery.Where(v => v.Estado.EsGanada).SumAsync(v => (decimal)v.MontoTotal);
            dto.Kpis.PendingCommissions = await comisionesQuery
                .Where(c => c.Estado.Codigo == "PENDIENTE")
                .SumAsync(c => (decimal)c.MontoComision);
            
            // Productos are always global
            dto.Kpis.TotalProductos = await _context.Productos.CountAsync();
            dto.Kpis.ActiveProducts = await _context.Productos.CountAsync(p => p.Cantidad > 0);

            // 5. Recent Sales (Top 5)
            dto.RecentSales = await ventasQuery
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .Include(v => v.TipoVenta)
                .OrderByDescending(v => v.FechaVenta)
                .Take(5)
                .Select(v => new RecentSaleDto
                {
                    Id = v.Id,
                    NumeroVenta = v.NumeroVenta,
                    MontoTotal = (decimal)v.MontoTotal,
                    FechaVenta = v.FechaVenta,
                    ClienteNombre = v.Cliente != null ? v.Cliente.Nombre : "Desconocido",
                    UsuarioNombre = v.Usuario != null ? v.Usuario.Nombre : "Desconocido",
                    Estado_Nombre = v.Estado.Nombre,
                    Estado_Color = v.Estado.Color,
                    TipoVenta_Nombre = v.TipoVenta != null ? v.TipoVenta.Nombre : "N/A",
                    OrigenVenta = v.OrigenVenta
                })
                .ToListAsync();

            // 6. Top Sellers
            var topSellersData = await ventasQuery
                .GroupBy(v => v.UsuarioId)
                .Select(g => new
                {
                    UsuarioId = g.Key,
                    TotalVendido = g.Sum(v => v.MontoTotal),
                    Count = g.Count()
                })
                .OrderByDescending(x => x.TotalVendido)
                .Take(5)
                .ToListAsync();

            var topUserIds = topSellersData.Select(t => t.UsuarioId).ToList();
            var users = await _context.Usuarios
                .Where(u => topUserIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u);

            foreach (var sellerData in topSellersData)
            {
                users.TryGetValue(sellerData.UsuarioId, out var user);
                dto.TopSellers.Add(new TopSellerDto
                {
                    UsuarioId = sellerData.UsuarioId,
                    Nombre = user?.Nombre ?? $"Vendedor {sellerData.UsuarioId}",
                    Email = user?.Email ?? "",
                    TotalVendido = (decimal)sellerData.TotalVendido,
                    Count = sellerData.Count
                });
            }

            return dto;
        }
    }
}
