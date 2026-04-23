using Microsoft.EntityFrameworkCore;
using CRMSarritelApi.Models;

namespace CRMSarritelApi.Data.Seeds
{
    public static class MunicipiosSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipio>().HasData(

                // Álava (01)
                new Municipio { Id = 1001, Nombre = "Alegría-Dulantzi", CodigoPostal = null, Activo = true, IdProvincia = 1 },
                new Municipio { Id = 1002, Nombre = "Amurrio", CodigoPostal = null, Activo = true, IdProvincia = 1 },
                new Municipio { Id = 1003, Nombre = "Añana", CodigoPostal = null, Activo = true, IdProvincia = 1 },
                new Municipio { Id = 1059, Nombre = "Artziniega", CodigoPostal = null, Activo = true, IdProvincia = 1 },
                new Municipio { Id = 1901, Nombre = "Vitoria-Gasteiz", CodigoPostal = null, Activo = true, IdProvincia = 1 },

                // Albacete (02)
                new Municipio { Id = 2001, Nombre = "Abengibre", CodigoPostal = null, Activo = true, IdProvincia = 2 },
                new Municipio { Id = 2002, Nombre = "Alatoz", CodigoPostal = null, Activo = true, IdProvincia = 2 },
                new Municipio { Id = 2003, Nombre = "Albacete", CodigoPostal = null, Activo = true, IdProvincia = 2 },
                new Municipio { Id = 2004, Nombre = "Albatana", CodigoPostal = null, Activo = true, IdProvincia = 2 },
                new Municipio { Id = 2901, Nombre = "Albacete", CodigoPostal = null, Activo = true, IdProvincia = 2 },

                // Alicante/Alacant (03)
                new Municipio { Id = 3001, Nombre = "Adsubia", CodigoPostal = null, Activo = true, IdProvincia = 3 },
                new Municipio { Id = 3002, Nombre = "Agost", CodigoPostal = null, Activo = true, IdProvincia = 3 },
                new Municipio { Id = 3003, Nombre = "Agres", CodigoPostal = null, Activo = true, IdProvincia = 3 },
                new Municipio { Id = 3013, Nombre = "Alicante/Alacant", CodigoPostal = null, Activo = true, IdProvincia = 3 },
                new Municipio { Id = 3901, Nombre = "Alicante/Alacant", CodigoPostal = null, Activo = true, IdProvincia = 3 },

                // Almería (04)
                new Municipio { Id = 4001, Nombre = "Abla", CodigoPostal = null, Activo = true, IdProvincia = 4 },
                new Municipio { Id = 4002, Nombre = "Abrucena", CodigoPostal = null, Activo = true, IdProvincia = 4 },
                new Municipio { Id = 4003, Nombre = "Adra", CodigoPostal = null, Activo = true, IdProvincia = 4 },
                new Municipio { Id = 4004, Nombre = "Albánchez", CodigoPostal = null, Activo = true, IdProvincia = 4 },
                new Municipio { Id = 4901, Nombre = "Almería", CodigoPostal = null, Activo = true, IdProvincia = 4 },

                // Barcelona (08)
                new Municipio { Id = 8001, Nombre = "Abrera", CodigoPostal = null, Activo = true, IdProvincia = 8 },
                new Municipio { Id = 8002, Nombre = "Aguilar de Segarra", CodigoPostal = null, Activo = true, IdProvincia = 8 },
                new Municipio { Id = 8003, Nombre = "Alella", CodigoPostal = null, Activo = true, IdProvincia = 8 },
                new Municipio { Id = 8004, Nombre = "Alpens", CodigoPostal = null, Activo = true, IdProvincia = 8 },
                new Municipio { Id = 8019, Nombre = "Barcelona", CodigoPostal = null, Activo = true, IdProvincia = 8 },
                new Municipio { Id = 8901, Nombre = "Barcelona", CodigoPostal = null, Activo = true, IdProvincia = 8 },

                // Madrid (28)
                new Municipio { Id = 28001, Nombre = "Acebeda (La)", CodigoPostal = null, Activo = true, IdProvincia = 28 },
                new Municipio { Id = 28002, Nombre = "Ajalvir", CodigoPostal = null, Activo = true, IdProvincia = 28 },
                new Municipio { Id = 28003, Nombre = "Alameda del Valle", CodigoPostal = null, Activo = true, IdProvincia = 28 },
                new Municipio { Id = 28004, Nombre = "Alcalá de Henares", CodigoPostal = null, Activo = true, IdProvincia = 28 },
                new Municipio { Id = 28005, Nombre = "Alcobendas", CodigoPostal = null, Activo = true, IdProvincia = 28 },
                new Municipio { Id = 28006, Nombre = "Alcorcón", CodigoPostal = null, Activo = true, IdProvincia = 28 },
                new Municipio { Id = 28079, Nombre = "Madrid", CodigoPostal = null, Activo = true, IdProvincia = 28 },
                new Municipio { Id = 28901, Nombre = "Madrid", CodigoPostal = null, Activo = true, IdProvincia = 28 },

                // Las Palmas (35)
                new Municipio { Id = 35001, Nombre = "Agaete", CodigoPostal = null, Activo = true, IdProvincia = 35 },
                new Municipio { Id = 35002, Nombre = "Agüimes", CodigoPostal = null, Activo = true, IdProvincia = 35 },
                new Municipio { Id = 35003, Nombre = "Antigua", CodigoPostal = null, Activo = true, IdProvincia = 35 },
                new Municipio { Id = 35013, Nombre = "Las Palmas de Gran Canaria", CodigoPostal = null, Activo = true, IdProvincia = 35 },
                new Municipio { Id = 35901, Nombre = "Las Palmas de Gran Canaria", CodigoPostal = null, Activo = true, IdProvincia = 35 },

                // Santa Cruz de Tenerife (38)
                new Municipio { Id = 38001, Nombre = "Adeje", CodigoPostal = null, Activo = true, IdProvincia = 38 },
                new Municipio { Id = 38002, Nombre = "Agulo", CodigoPostal = null, Activo = true, IdProvincia = 38 },
                new Municipio { Id = 38003, Nombre = "Alajeró", CodigoPostal = null, Activo = true, IdProvincia = 38 },
                new Municipio { Id = 38022, Nombre = "Santa Cruz de Tenerife", CodigoPostal = null, Activo = true, IdProvincia = 38 },
                new Municipio { Id = 38901, Nombre = "Santa Cruz de Tenerife", CodigoPostal = null, Activo = true, IdProvincia = 38 },

                // Ceuta (51)
                new Municipio { Id = 51001, Nombre = "Ceuta", CodigoPostal = null, Activo = true, IdProvincia = 51 },
                new Municipio { Id = 51901, Nombre = "Ceuta", CodigoPostal = null, Activo = true, IdProvincia = 51 },

                // Melilla (52)
                new Municipio { Id = 52001, Nombre = "Melilla", CodigoPostal = null, Activo = true, IdProvincia = 52 },
                new Municipio { Id = 52901, Nombre = "Melilla", CodigoPostal = null, Activo = true, IdProvincia = 52 }

                // ← Aquí irían el resto de municipios (más de 8000 líneas en el caso completo)
            );
        }
    }
}