using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMSarritelApi.Data.Seeds
{
    public static class CategoriasSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Fijo / Fibra" },
                new Categoria { Id = 2, Nombre = "Móvil" },
                new Categoria { Id = 3, Nombre = "Televisión" },
                new Categoria { Id = 4, Nombre = "Seguridad / Alarmas" },
                new Categoria { Id = 5, Nombre = "Energía" },
                new Categoria { Id = 6, Nombre = "Otros" }
            );
        }
    }
}
