using System;

namespace CRMSarritelApi.Models
{
    public class EstadoComisionValue
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Color { get; set; } = "badge-neutral";
        public string Icono { get; set; } = "⏳";
        public bool EsPagable { get; set; } = false;
        public bool EsFinal { get; set; } = false;
        public short Orden { get; set; } = 10;

        public EstadoComisionValue() { }

        public EstadoComisionValue(
            string codigo,
            string nombre,
            string color = "badge-neutral",
            string icono = "⏳",
            bool esPagable = false,
            bool esFinal = false,
            short orden = 10)
        {
            Codigo = codigo ?? throw new ArgumentNullException(nameof(codigo));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Color = color;
            Icono = icono;
            EsPagable = esPagable;
            EsFinal = esFinal;
            Orden = orden;
        }

        public static EstadoComisionValue Inactiva => new("INAC", "Inactiva", "badge-ghost", "⏸️", false, false, 5);
        public static EstadoComisionValue Pendiente => new("PEND", "Pendiente de pago", "badge-warning", "⏳", false, false, 10);
        public static EstadoComisionValue Activa => new("ACTI", "Activa", "badge-primary", "✨", true, false, 15);
        public static EstadoComisionValue Calculada => new("CALC", "Calculada", "badge-info", "🔢", false, false, 20);
        public static EstadoComisionValue Aprobada => new("APRO", "Aprobada", "badge-success", "✅", true, false, 30);
        public static EstadoComisionValue Pagada => new("PAGA", "Pagada", "badge-success", "💰", false, true, 90);
        public static EstadoComisionValue Rechazada => new("RECH", "Rechazada", "badge-error", "❌", false, true, 100);
        public static EstadoComisionValue Cancelada => new("CANC", "Cancelada", "badge-error", "🗑️", false, true, 110);
    }
}
