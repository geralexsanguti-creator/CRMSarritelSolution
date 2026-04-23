using Microsoft.EntityFrameworkCore;
using CRMSarritelApi.Models;

namespace CRMSarritelApi.Data.Seeds
{
    public static class ProvinciasSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provincia>().HasData(
                new Provincia { Id = 1, Nombre = "Araba/Álava", Codigo = "01" },
                new Provincia { Id = 2, Nombre = "Albacete", Codigo = "02" },
                new Provincia { Id = 3, Nombre = "Alacant/Alicante", Codigo = "03" },
                new Provincia { Id = 4, Nombre = "Almería", Codigo = "04" },
                new Provincia { Id = 5, Nombre = "Ávila", Codigo = "05" },
                new Provincia { Id = 6, Nombre = "Badajoz", Codigo = "06" },
                new Provincia { Id = 7, Nombre = "Illes Balears", Codigo = "07" },
                new Provincia { Id = 8, Nombre = "Barcelona", Codigo = "08" },
                new Provincia { Id = 9, Nombre = "Burgos", Codigo = "09" },
                new Provincia { Id = 10, Nombre = "Cáceres", Codigo = "10" },
                new Provincia { Id = 11, Nombre = "Cádiz", Codigo = "11" },
                new Provincia { Id = 12, Nombre = "Castelló/Castellón", Codigo = "12" },
                new Provincia { Id = 13, Nombre = "Ciudad Real", Codigo = "13" },
                new Provincia { Id = 14, Nombre = "Córdoba", Codigo = "14" },
                new Provincia { Id = 15, Nombre = "A Coruña", Codigo = "15" },
                new Provincia { Id = 16, Nombre = "Cuenca", Codigo = "16" },
                new Provincia { Id = 17, Nombre = "Girona", Codigo = "17" },
                new Provincia { Id = 18, Nombre = "Granada", Codigo = "18" },
                new Provincia { Id = 19, Nombre = "Guadalajara", Codigo = "19" },
                new Provincia { Id = 20, Nombre = "Gipuzkoa", Codigo = "20" },
                new Provincia { Id = 21, Nombre = "Huelva", Codigo = "21" },
                new Provincia { Id = 22, Nombre = "Huesca", Codigo = "22" },
                new Provincia { Id = 23, Nombre = "Jaén", Codigo = "23" },
                new Provincia { Id = 24, Nombre = "León", Codigo = "24" },
                new Provincia { Id = 25, Nombre = "Lleida", Codigo = "25" },
                new Provincia { Id = 26, Nombre = "La Rioja", Codigo = "26" },
                new Provincia { Id = 27, Nombre = "Lugo", Codigo = "27" },
                new Provincia { Id = 28, Nombre = "Madrid", Codigo = "28" },
                new Provincia { Id = 29, Nombre = "Málaga", Codigo = "29" },
                new Provincia { Id = 30, Nombre = "Murcia", Codigo = "30" },
                new Provincia { Id = 31, Nombre = "Navarra", Codigo = "31" },
                new Provincia { Id = 32, Nombre = "Ourense", Codigo = "32" },
                new Provincia { Id = 33, Nombre = "Asturias", Codigo = "33" },
                new Provincia { Id = 34, Nombre = "Palencia", Codigo = "34" },
                new Provincia { Id = 35, Nombre = "Las Palmas", Codigo = "35" },
                new Provincia { Id = 36, Nombre = "Pontevedra", Codigo = "36" },
                new Provincia { Id = 37, Nombre = "Salamanca", Codigo = "37" },
                new Provincia { Id = 38, Nombre = "Santa Cruz de Tenerife", Codigo = "38" },
                new Provincia { Id = 39, Nombre = "Cantabria", Codigo = "39" },
                new Provincia { Id = 40, Nombre = "Segovia", Codigo = "40" },
                new Provincia { Id = 41, Nombre = "Sevilla", Codigo = "41" },
                new Provincia { Id = 42, Nombre = "Soria", Codigo = "42" },
                new Provincia { Id = 43, Nombre = "Tarragona", Codigo = "43" },
                new Provincia { Id = 44, Nombre = "Teruel", Codigo = "44" },
                new Provincia { Id = 45, Nombre = "Toledo", Codigo = "45" },
                new Provincia { Id = 46, Nombre = "Valencia/València", Codigo = "46" },
                new Provincia { Id = 47, Nombre = "Valladolid", Codigo = "47" },
                new Provincia { Id = 48, Nombre = "Bizkaia", Codigo = "48" },
                new Provincia { Id = 49, Nombre = "Zamora", Codigo = "49" },
                new Provincia { Id = 50, Nombre = "Zaragoza", Codigo = "50" },
                new Provincia { Id = 51, Nombre = "Ceuta", Codigo = "51" },
                new Provincia { Id = 52, Nombre = "Melilla", Codigo = "52" }
            );
        }
    }
}
