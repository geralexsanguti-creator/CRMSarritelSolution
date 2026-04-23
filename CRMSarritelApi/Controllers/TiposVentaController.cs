using CRMSarritelApi.Models;
using CRMSarritelApi.Repositories;
using CRMSarritelApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TiposVentaController(IGenericRepository<TipoVenta, int> repository) : ControllerBase
    {
        private readonly IGenericRepository<TipoVenta, int> _repository = repository;

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
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] TipoVentaDto dto)
        {
            var tipo = new TipoVenta
            {
                Nombre = dto.Nombre ?? string.Empty,
                Codigo = dto.Codigo ?? string.Empty,
                Descripcion = dto.Descripcion,
                Activo = dto.Activo,
                EsquemaVariablesJson = dto.EsquemaVariablesJson,
                EstadosVentaJson = dto.EstadosVentaJson,
                OrigenesJson = dto.OrigenesJson
            };

            await _repository.Insertar(tipo);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = tipo.Id }, tipo);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(int id, [FromBody] TipoVentaDto dto)
        {
            if (string.IsNullOrEmpty(dto.Nombre))
            {
                Console.WriteLine($"DEBUG ERROR: Received empty Nombre for ID {id}. Full DTO: {System.Text.Json.JsonSerializer.Serialize(dto)}");
                // return BadRequest(new { errors = new { Nombre = new[] { "The Nombre field is required (Manual Check)." } } });
            }

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Nombre = dto.Nombre ?? "SIN NOMBRE";
            existing.Codigo = dto.Codigo;
            existing.Descripcion = dto.Descripcion;
            existing.Activo = dto.Activo;
            existing.EsquemaVariablesJson = dto.EsquemaVariablesJson;
            existing.EstadosVentaJson = dto.EstadosVentaJson;
            existing.OrigenesJson = dto.OrigenesJson;

            _repository.Actualizar(existing);
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
