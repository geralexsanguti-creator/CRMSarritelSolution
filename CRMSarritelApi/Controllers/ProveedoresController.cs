using CRMSarritelApi.Models;
using CRMSarritelApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProveedoresController(
        IGenericRepository<Proveedor, int> repository,
        IGenericRepository<TipoVenta, int> tvRepository) : ControllerBase
    {
        private readonly IGenericRepository<Proveedor, int> _repository = repository;
        private readonly IGenericRepository<TipoVenta, int> _tvRepository = tvRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAll()
                .Include(p => p.TiposVenta)
                .ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetAll()
                .Include(p => p.TiposVenta)
                .Include(p => p.Reglas)
                .FirstOrDefaultAsync(p => p.Id == id);
                
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Proveedor proveedor)
        {
            if (proveedor.TipoVentaIds != null && proveedor.TipoVentaIds.Count > 0)
            {
                var tvs = await _tvRepository.GetAll()
                    .Where(tv => proveedor.TipoVentaIds.Contains(tv.Id))
                    .ToListAsync();
                proveedor.TiposVenta = tvs;
            }

            await _repository.Insertar(proveedor);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = proveedor.Id }, proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Proveedor proveedor)
        {
            if (proveedor.Id == 0) proveedor.Id = id;
            if (id != proveedor.Id) return BadRequest();

            // Para actualizaciones con relaciones Many-to-Many, necesitamos cargar la entidad existente
            var existing = await _repository.GetAll()
                .Include(p => p.TiposVenta)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existing == null) return NotFound();

            // Mapear campos básicos
            existing.Nombre = proveedor.Nombre;
            existing.NombrePlataforma = proveedor.NombrePlataforma;
            existing.CIF = proveedor.CIF;
            existing.Web = proveedor.Web;
            existing.EmailContacto = proveedor.EmailContacto;
            existing.Telefono = proveedor.Telefono;
            existing.LogoUrl = proveedor.LogoUrl;
            existing.Activo = proveedor.Activo;

            // Sincronizar TiposVenta
            if (proveedor.TipoVentaIds != null)
            {
                existing.TiposVenta.Clear();
                var tvs = await _tvRepository.GetAll()
                    .Where(tv => proveedor.TipoVentaIds.Contains(tv.Id))
                    .ToListAsync();
                foreach (var tv in tvs) existing.TiposVenta.Add(tv);
            }

            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DuroBorrado(id);
            if (!success) return NotFound();
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
