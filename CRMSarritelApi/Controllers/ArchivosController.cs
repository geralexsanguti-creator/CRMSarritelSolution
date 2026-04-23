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
    public class ArchivosController(IGenericRepository<ArchivoAdjunto, int> repository) : ControllerBase
    {
        private readonly IGenericRepository<ArchivoAdjunto, int> _repository = repository;

        // GET: api/Archivos/entidad/{entidadTipo}/{entidadId}
        [HttpGet("entidad/{entidadTipo}/{entidadId}")]
        public async Task<IActionResult> GetArchivosPorEntidad(string entidadTipo, int entidadId)
        {
            var archivos = await _repository.GetAll()
                .Where(a => a.EntidadTipo == entidadTipo && a.EntidadId == entidadId)
                .Select(a => new
                {
                    a.Id,
                    a.EntidadTipo,
                    a.EntidadId,
                    a.NombreArchivo,
                    a.TipoMime,
                    a.TamanoBytes,
                    a.Descripcion,
                    a.FechaCreacion,
                    a.CreadoPorId
                })
                .ToListAsync();

            return Ok(archivos);
        }

        // GET: api/Archivos/descargar/{id}
        [HttpGet("descargar/{id}")]
        public async Task<IActionResult> DescargarArchivo(int id)
        {
            var archivo = await _repository.GetByIdAsync(id);
            if (archivo == null || archivo.ArchivoBytes == null || archivo.ArchivoBytes.Length == 0)
                return NotFound();

            // Return file exactly as it was uploaded to be viewed in browser if possible, or downloaded
            return File(archivo.ArchivoBytes, archivo.TipoMime, archivo.NombreArchivo);
        }

        // POST: api/Archivos
        [HttpPost]
        public async Task<IActionResult> SubirArchivo([FromForm] string entidadTipo, [FromForm] int entidadId, [FromForm] string? descripcion, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Ningún archivo fue enviado.");

            if (string.IsNullOrEmpty(entidadTipo) || entidadId <= 0)
                return BadRequest("EntidadTipo y EntidadId son obligatorios.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();

            var archivoAdjunto = new ArchivoAdjunto
            {
                EntidadTipo = entidadTipo,
                EntidadId = entidadId,
                NombreArchivo = file.FileName,
                TipoMime = file.ContentType,
                TamanoBytes = file.Length,
                ArchivoBytes = bytes,
                Descripcion = descripcion,
                FechaCreacion = DateTime.UtcNow
                // CreadoPorId podría sacarse del Token JWT del User.Identity
            };

            await _repository.Insertar(archivoAdjunto);
            await _repository.SaveChangesAsync();

            // Return safe object without the payload
            return Ok(new
            {
                archivoAdjunto.Id,
                archivoAdjunto.NombreArchivo,
                archivoAdjunto.TipoMime,
                archivoAdjunto.TamanoBytes,
                archivoAdjunto.FechaCreacion
            });
        }

        // DELETE: api/Archivos/{id}
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
