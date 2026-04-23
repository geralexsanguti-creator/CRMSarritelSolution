using CRMSarritelApi.Models;
using CRMSarritelApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CRMSarritelApi.Data;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ProductosController(IGenericRepository<Producto, int> repository, CRMSarritelDbContext context) : ControllerBase
    {
        private readonly IGenericRepository<Producto, int> _repository = repository;
        private readonly CRMSarritelDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAll()
                .Include(p => p.TipoVenta)
                .Include(p => p.Proveedor)
                .Include(p => p.ProductoCarpetas)
                    .ThenInclude(pc => pc.CarpetaReglas)
                .ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetAll()
                .Include(p => p.TipoVenta)
                .Include(p => p.Proveedor)
                .Include(p => p.ProductoCarpetas)
                    .ThenInclude(pc => pc.CarpetaReglas)
                .FirstOrDefaultAsync(p => p.Id == id);
                
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] Producto producto)
        {
            if (producto.Activo && producto.FechaActivacion == null)
            {
                producto.FechaActivacion = DateTime.UtcNow;
            }
            await _repository.Insertar(producto);

            if (producto.CarpetaIds != null && producto.CarpetaIds.Count > 0)
            {
                foreach (var carpetaId in producto.CarpetaIds)
                {
                    producto.ProductoCarpetas.Add(new ProductoCarpeta { CarpetaReglasId = carpetaId });
                }
            }

            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
        {
            if (producto.Id == 0) producto.Id = id;
            if (id != producto.Id) return BadRequest();

            var dbItem = await _repository.GetAll()
                .Include(p => p.ProductoCarpetas)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (dbItem == null) return NotFound();

            dbItem.Nombre = producto.Nombre;
            dbItem.Descripcion = producto.Descripcion;
            dbItem.Precio = producto.Precio;
            dbItem.PrecioOferta = producto.PrecioOferta;
            dbItem.Cantidad = producto.Cantidad;
            dbItem.Imagen = producto.Imagen;
            dbItem.TipoVentaId = producto.TipoVentaId;
            dbItem.ProveedorId = producto.ProveedorId;
            dbItem.EsInfinito = producto.EsInfinito;
            dbItem.FechaLimite = producto.FechaLimite;
            dbItem.Activo = producto.Activo;
            dbItem.FechaActivacion = producto.FechaActivacion;
            dbItem.FechaBaja = producto.FechaBaja;

            // Logica de Activacion Autómatica si no habia fecha
            if (dbItem.Activo && dbItem.FechaActivacion == null)
            {
                dbItem.FechaActivacion = DateTime.UtcNow;
            }
            
            // Si se desactiva y no tiene fecha de baja, ponersela
            if (!dbItem.Activo && dbItem.FechaBaja == null)
            {
                dbItem.FechaBaja = DateTime.UtcNow;
            }

            // Sincronizar Carpetas
            if (producto.CarpetaIds != null)
            {
                var requestedCarpetas = producto.CarpetaIds.Distinct().ToList();
                var currentCarpetas = await _context.ProductoCarpetas.Where(pc => pc.ProductoId == id).ToListAsync();

                foreach (var cc in currentCarpetas)
                {
                    if (!requestedCarpetas.Contains(cc.CarpetaReglasId))
                    {
                        _context.ProductoCarpetas.Remove(cc);
                    }
                }

                foreach (var carpetaId in requestedCarpetas)
                {
                    if (!currentCarpetas.Any(cc => cc.CarpetaReglasId == carpetaId) && carpetaId > 0)
                    {
                        _context.ProductoCarpetas.Add(new ProductoCarpeta { ProductoId = id, CarpetaReglasId = carpetaId });
                    }
                }
            }

            await _repository.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Productos.FindAsync(id);
            if (product == null) return NotFound();

            // Verificar si hay ventas asociadas
            var hasSales = await _context.DetalleVentas.AnyAsync(d => d.IdProducto == id);

            if (hasSales)
            {
                // Soft Delete
                product.Activo = false;
                product.FechaBaja = DateTime.UtcNow;
                _context.Update(product);
            }
            else
            {
                // Duro borrado si no hay ventas
                await _repository.DuroBorrado(id);
            }
            
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{id}/rules")]
        public async Task<IActionResult> GetRules(int id)
        {
            var product = await _context.Productos
                .Include(p => p.ProductoCarpetas)
                    .ThenInclude(pc => pc.CarpetaReglas)
                        .ThenInclude(cr => cr!.CarpetaReglasComision)
                            .ThenInclude(crc => crc.ReglaComision)
                                .ThenInclude(r => r!.Tiers)
                .Include(p => p.ProductoCarpetas)
                    .ThenInclude(pc => pc.CarpetaReglas)
                        .ThenInclude(cr => cr!.CarpetaReglasComision)
                            .ThenInclude(crc => crc.ReglaComision)
                                .ThenInclude(r => r!.ReparticionesBase)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            var rules = product.ProductoCarpetas
                .Where(pc => pc.CarpetaReglas != null && pc.CarpetaReglas.Activo)
                .SelectMany(pc => pc.CarpetaReglas?.CarpetaReglasComision ?? new List<CarpetaReglaComision>())
                .Select(crc => crc.ReglaComision)
                .Where(r => r != null && r.Activa)
                .ToList();

            return Ok(rules);
        }
    }
}
