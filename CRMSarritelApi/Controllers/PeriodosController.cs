using CRMSarritelApi.Models;
using CRMSarritelApi.Repositories;
using CRMSarritelApi.Data;
using CRMSarritelApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PeriodosController(
        IGenericRepository<PeriodoFacturacion, int> repository,
        ICommissionService commissionService,
        CRMSarritelDbContext context) : ControllerBase
    {
        private readonly IGenericRepository<PeriodoFacturacion, int> _repository = repository;
        private readonly ICommissionService _commissionService = commissionService;
        private readonly CRMSarritelDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAll()
                .OrderByDescending(p => p.FechaInicio)
                .ToListAsync();
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
        public async Task<IActionResult> Create(PeriodoFacturacion periodo)
        {
            await _repository.Insertar(periodo);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = periodo.Id }, periodo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PeriodoFacturacion periodo)
        {
            if (id != periodo.Id) return BadRequest();
            _repository.Actualizar(periodo);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/set-principal")]
        public async Task<IActionResult> SetPrincipal(int id)
        {
            var periodo = await _context.PeriodosFacturacion.FindAsync(id);
            if (periodo == null) return NotFound();

            // Desmarcar todos los demás
            var others = await _context.PeriodosFacturacion.Where(p => p.EsPrincipal && p.Id != id).ToListAsync();
            foreach (var o in others) o.EsPrincipal = false;

            periodo.EsPrincipal = true;
            await _context.SaveChangesAsync();
            
            return Ok(new { message = "Periodo establecido como principal" });
        }

        [HttpPost("{id}/recalculate")]
        public async Task<IActionResult> Recalculate(int id)
        {
            await _commissionService.RecalculatePeriod(id);
            return Ok(new { message = "Recálculo completado" });
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
