using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSarritelApi.Models
{
    public class Venta : BaseEntity<int>
    {
        public int VentaId { get; set; }
        public string NumeroVenta { get; set; } = string.Empty;

        // Relación con el nuevo modelo dinámico de Tipos de Venta
        public int? TipoVentaId { get; set; }
        public virtual TipoVenta? TipoVenta { get; set; }

        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }

        public int? ProductoPrincipalId { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaVenta { get; set; } = DateTime.UtcNow.Date;

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaInstalacionPrevista { get; set; }
        public DateTime? FechaInstalacionReal { get; set; }

        // Reemplazamos EstadoId + Estado por value object embebido
        public EstadoVentaValue Estado { get; set; } = new EstadoVentaValue();

        public short EtapaActual { get; set; } = 1;

        public decimal MontoVenta { get; set; } = 0;
        public decimal DescuentoPorcentaje { get; set; } = 0;
        public decimal DescuentoMonto { get; set; } = 0;
        public decimal MontoTotal { get; set; } = 0;

        public string? Notas { get; set; }
        public string? ArchivoContrato { get; set; }

        public int? CreadoPorId { get; set; }
        public string OrigenVenta { get; set; } = "presencial";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relaciones con otras entidades (siguen igual)
        public virtual Cliente? Cliente { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public virtual Producto? ProductoPrincipal { get; set; }
        public virtual Usuario? CreadoPor { get; set; }

        public virtual ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
        public virtual ICollection<Comision> Comisiones { get; set; } = new List<Comision>();
    }


    // ────────────────────────────────────────────────
    // Value Object para Estado de Venta (embebido)
    // ────────────────────────────────────────────────
    public class EstadoVentaValue
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Icono { get; set; } = "📋";
        public string Color { get; set; } = "badge-neutral";
        public short Orden { get; set; } = 10;
        public bool PermiteEdicion { get; set; } = true;
        public bool PermiteEliminar { get; set; } = true;
        public bool EsFinal { get; set; } = false;
        public bool Activo { get; set; } = true;
        public bool EsInicial { get; set; } = false;
        public bool EsGanada { get; set; } = false;

        public EstadoVentaValue() { }

        // Constructor conveniente (opcional)
        public EstadoVentaValue(
            string codigo,
            string nombre,
            string icono = "📋",
            string color = "badge-neutral",
            short orden = 10,
            bool permiteEdicion = true,
            bool permiteEliminar = true,
            bool esFinal = false,
            bool activo = true,
            bool esInicial = false,
            bool esGanada = false)
        {
            Codigo = codigo ?? throw new ArgumentNullException(nameof(codigo));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Icono = icono;
            Color = color;
            Orden = orden;
            PermiteEdicion = permiteEdicion;
            PermiteEliminar = permiteEliminar;
            EsFinal = esFinal;
            Activo = activo;
            EsInicial = esInicial;
            EsGanada = esGanada;
        }
    }



}
