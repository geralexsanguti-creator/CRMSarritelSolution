using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSarritelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public FilesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var allowedExtensions = new[] { 
                ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg", 
                ".pdf", 
                ".mp4", ".avi", ".mov", ".mkv", ".webm" 
            };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                return BadRequest("Solo se permiten imágenes, videos y documentos PDF.");

            if (file.Length > 50 * 1024 * 1024) // 50 MB limit
                return BadRequest("El archivo supera el límite de 50 MB.");

            // Create uploads directory if it doesn't exist
            string uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate a unique filename
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName.Replace(" ", "_");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return just the filename or the relative url
            return Ok(new { url = uniqueFileName });
        }
    }
}
