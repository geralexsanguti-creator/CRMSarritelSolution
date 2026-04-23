using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace CRMSarritelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguracionController : ControllerBase
    {
        private readonly string _settingsFilePath;

        public ConfiguracionController()
        {
            var currentDir = Directory.GetCurrentDirectory();
            _settingsFilePath = Path.Combine(currentDir, "pipeline-settings.json");
            
            // Create default if not exists
            if (!System.IO.File.Exists(_settingsFilePath))
            {
                var defaultSettings = new PipelineConfig
                {
                    TiposVenta = new[] {
                        new TipoVentaConfig { Codigo = "NUEVA ALTA", Nombre = "Nueva Alta" },
                        new TipoVentaConfig { Codigo = "MIGRACION", Nombre = "Migración" },
                        new TipoVentaConfig { Codigo = "UPSELL", Nombre = "Upsell / Cross-sell" },
                        new TipoVentaConfig { Codigo = "RENOVACION", Nombre = "Renovación" }
                    },
                    EstadosVenta = new[] {
                        new EstadoVentaConfig { Codigo = "PENDIENTE", Nombre = "Pendiente", Color = "badge-neutral", Icono = "📋", Orden = 10, EsInicial = true, EsFinal = false },
                        new EstadoVentaConfig { Codigo = "EN_PROCESO", Nombre = "En Proceso", Color = "badge-info", Icono = "⏳", Orden = 20, EsInicial = false, EsFinal = false },
                        new EstadoVentaConfig { Codigo = "COMPLETADA", Nombre = "Completada", Color = "badge-success", Icono = "✅", Orden = 30, EsInicial = false, EsFinal = true },
                        new EstadoVentaConfig { Codigo = "CANCELADA", Nombre = "Cancelada", Color = "badge-error", Icono = "❌", Orden = 99, EsInicial = false, EsFinal = true }
                    }
                };
                System.IO.File.WriteAllText(_settingsFilePath, JsonSerializer.Serialize(defaultSettings));
            }
        }

        [HttpGet("Pipeline")]
        public async Task<IActionResult> GetPipelineConfig()
        {
            try
            {
                var json = await System.IO.File.ReadAllTextAsync(_settingsFilePath);
                var config = JsonSerializer.Deserialize<PipelineConfig>(json);
                return Ok(config);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "Error leyendo la configuración", details = ex.Message });
            }
        }

        [HttpPost("Pipeline")]
        public async Task<IActionResult> SavePipelineConfig([FromBody] PipelineConfig config)
        {
            try
            {
                var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                await System.IO.File.WriteAllTextAsync(_settingsFilePath, json);
                return Ok(config);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "Error guardando la configuración", details = ex.Message });
            }
        }
    }

    public class PipelineConfig
    {
        public TipoVentaConfig[] TiposVenta { get; set; } = System.Array.Empty<TipoVentaConfig>();
        public EstadoVentaConfig[] EstadosVenta { get; set; } = System.Array.Empty<EstadoVentaConfig>();
    }

    public class TipoVentaConfig
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
    }

    public class EstadoVentaConfig
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Color { get; set; } = "badge-neutral";
        public string Icono { get; set; } = "📋";
        public int Orden { get; set; } = 10;
        public bool EsInicial { get; set; } = false;
        public bool EsFinal { get; set; } = false;
    }
}
