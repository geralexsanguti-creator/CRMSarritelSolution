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
    public class CategoriasController(IGenericRepository<Categoria, int> repository) : ControllerBase
    {
        private readonly IGenericRepository<Categoria, int> _repository = repository;

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
        public async Task<IActionResult> Create(Categoria categoria)
        {
            await _repository.Insertar(categoria);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Categoria categoria)
        {
            if (id != categoria.Id) return BadRequest();
            _repository.Actualizar(categoria);
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
