using System;

namespace CRMSarritelApi.Models
{
    public class TipoComisionValue
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

        public TipoComisionValue() { }

        public TipoComisionValue(string codigo, string nombre)
        {
            Codigo = codigo ?? throw new ArgumentNullException(nameof(codigo));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        }

        public static TipoComisionValue VentaCerrada => new("VENTA_CERRADA", "Comisión por venta cerrada");
        public static TipoComisionValue VentaInstalada => new("VENTA_INSTALADA", "Comisión por instalación");
        public static TipoComisionValue Bono => new("BONO", "Bono especial");
        public static TipoComisionValue Manual => new("MANUAL", "Comisión manual");
        public static TipoComisionValue Extraida => new("EXTRAIDA", "Comisión extraída (split)");
        public static TipoComisionValue GestionEquipo => new("GESTION_EQUIPO", "Gestión de Equipo");
    }
}
