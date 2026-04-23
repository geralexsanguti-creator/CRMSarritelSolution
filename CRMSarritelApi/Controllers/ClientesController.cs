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
    public class ClientesController(IGenericRepository<Cliente, int> repository) : ControllerBase
    {
        private readonly IGenericRepository<Cliente, int> _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAll().ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            await _repository.Insertar(cliente);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest();
            _repository.Actualizar(cliente);
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

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductsByClient(int id, [FromServices] IGenericRepository<Venta, int> ventaRepository)
        {
            // Obtener todas las ventas del cliente incluyendo producto principal y detalles
            var ventas = await ventaRepository.GetAll()
                .Where(v => v.ClienteId == id)
                .Include(v => v.ProductoPrincipal)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .ToListAsync();

            if (ventas == null) return Ok(new List<Producto>());

            // Consolidar productos únicos (producto principal + productos en detalles)
            var allProducts = ventas
                .SelectMany(v => {
                    var products = new List<Producto>();
                    if (v.ProductoPrincipal != null) products.Add(v.ProductoPrincipal);
                    if (v.Detalles != null) 
                    {
                        products.AddRange(v.Detalles.Select(d => d.Producto).Where(p => p != null));
                    }
                    return products;
                })
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();

            return Ok(allProducts);
        }
    }
}
