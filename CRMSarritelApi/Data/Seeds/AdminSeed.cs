using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

namespace CRMSarritelApi.Data.Seeds
{
    public static class AdminSeed
    {
        private static readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();
        // Define a fixed seed date, e.g., when the application was first created or a known launch date
        private static readonly DateTime SeedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static void Seed(ModelBuilder modelBuilder)
        {
            var roles = new Rol[]
            {
                new Rol { Id = 1, Nombre = "Admin" },
                new Rol { Id = 2, Nombre = "Backoffice" },
                new Rol { Id = 3, Nombre = "Director" },
                new Rol { Id = 4, Nombre = "Jefe de Equipo" },
                new Rol { Id = 5, Nombre = "Comercial" },
                new Rol { Id = 6, Nombre = "Colaborador" },
                new Rol { Id = 7, Nombre = "Autónomo" }
            };

            var usuario = new Usuario()
            {
                Id = 1,
                Nombre = "Administrador",
                Username = "admin",
                Email = "admin@crmsarritel.com",
                // Use standard PasswordHasher
                PasswordHash = _passwordHasher.HashPassword(null!, "admin"),
                FechaCreation = SeedDate
            };

            var sistema = new Usuario()
            {
                Id = 99,
                Nombre = "SISTEMA (Organización)",
                Username = "sistema",
                Email = "sistema@crmsarritel.com",
                PasswordHash = _passwordHasher.HashPassword(null!, "crm_sarritel_system_secret_key_2024"),
                FechaCreation = SeedDate,
                Activo = true
            };

            var usuariorol = new UsuarioRol()
            {
                UsuarioId = usuario.Id,
                RolId = 1, // Admin role
                FechaAsignacion = SeedDate
            };

            var sistemaRol = new UsuarioRol()
            {
                UsuarioId = sistema.Id,
                RolId = 1, // Admin role for visibility if needed, though it's internal
                FechaAsignacion = SeedDate
            };

            // Módulos con CRUD estándar (Read, Create, Update, Delete)
            var crudModulos = new[] { "Clientes", "Usuarios", "Roles", "Equipos", "Fichajes", "Productos", "Proveedores", "Tipos-Venta", "Ventas", "Comisiones", "Contratos" };
            var acciones = new[] { "read", "create", "update", "delete" };
            
            var permisos = new List<Permiso>();
            int pId = 1000;

            // 1. Módulos CRUD
            foreach (var mod in crudModulos)
            {
                foreach (var acc in acciones)
                {
                    permisos.Add(new Permiso { Id = pId++, Modulo = mod, Nombre = $"{mod.ToLower()}:{acc}", Descripcion = $"Permiso para {acc} en el módulo {mod}" });
                }
            }

            // 2. Módulos Solo Lectura
            var readonlyModulos = new[] { "Dashboard" };
            foreach (var mod in readonlyModulos)
            {
                permisos.Add(new Permiso { Id = pId++, Modulo = mod, Nombre = $"{mod.ToLower()}:read", Descripcion = $"Permiso de visualización en el módulo {mod}" });
            }

            // 3. Permisos Especiales / Autoridad Máxima
            var todosModulos = crudModulos.Concat(readonlyModulos).Distinct();
            foreach (var mod in todosModulos)
            {
                permisos.Add(new Permiso { Id = pId++, Modulo = mod, Nombre = $"{mod.ToLower()}:view_all", Descripcion = $"Permiso de visibilidad global sin restricciones de equipo en {mod}" });
            }

            var rolPermisos = permisos.Select(p => new RolPermiso { RolId = 1, PermisoId = p.Id }).ToList();

            modelBuilder.Entity<Rol>().HasData(roles);
            modelBuilder.Entity<Usuario>().HasData(usuario, sistema);
            modelBuilder.Entity<UsuarioRol>().HasData(usuariorol, sistemaRol);
            modelBuilder.Entity<Permiso>().HasData(permisos);
            modelBuilder.Entity<RolPermiso>().HasData(rolPermisos);
        }
    }
}