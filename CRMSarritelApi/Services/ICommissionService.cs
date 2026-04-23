using System.Collections.Generic;
using System.Threading.Tasks;
using CRMSarritelApi.Models;

namespace CRMSarritelApi.Services
{
    public interface ICommissionService
    {
        /// <summary>
        /// Calcula y genera los registros de comisión para una venta específica.
        Task CalculateCommissionsForSale(int ventaId, bool ignoreManual = false);

        /// <summary>
        /// Recalcula todas las comisiones de un periodo específico (ej: cierre mensual).
        /// </summary>
        Task RecalculatePeriod(int periodoId);

        /// <summary>
        /// Obtiene el margen neto proyectado para una regla antes de guardarla (Validación).
        /// </summary>
        decimal GetProjectedMargin(int reglaId);
    }
}
