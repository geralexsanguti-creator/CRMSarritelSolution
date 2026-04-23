using CRMSarritelApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMSarritelApi.Data
{
    public static class DbSeeder
    {
        public static async Task SeedPermissionsAsync(CRMSarritelDbContext context)
        {
            var permissions = new List<Permiso>
            {
                // Ventas
                new Permiso { Nombre = "ventas:view", Modulo = "Ventas", Descripcion = "Ver listado de ventas" },
                new Permiso { Nombre = "ventas:edit", Modulo = "Ventas", Descripcion = "Crear o editar ventas" },
                new Permiso { Nombre = "ventas:delete", Modulo = "Ventas", Descripcion = "Borrar ventas" },
                
                // Clientes
                new Permiso { Nombre = "clientes:view", Modulo = "Clientes", Descripcion = "Ver listado de clientes" },
                new Permiso { Nombre = "clientes:edit", Modulo = "Clientes", Descripcion = "Gestionar clientes" },

                // Usuarios
                new Permiso { Nombre = "usuarios:view", Modulo = "Usuarios", Descripcion = "Ver listado de usuarios" },
                new Permiso { Nombre = "usuarios:manage", Modulo = "Usuarios", Descripcion = "Gestionar usuarios y roles" },

                // Comisiones (Granulares)
                new Permiso { Nombre = "comisiones:view", Modulo = "Comisiones", Descripcion = "Ver comisiones propias (Gral)" },
                new Permiso { Nombre = "comisiones:view_personal", Modulo = "Comisiones", Descripcion = "Ver vista Personal / Mi Equipo" },
                new Permiso { Nombre = "comisiones:view_users", Modulo = "Comisiones", Descripcion = "Ver auditoría de Usuarios" },
                new Permiso { Nombre = "comisiones:view_admin", Modulo = "Comisiones", Descripcion = "Ver métricas de Empresa / Admin" },
                new Permiso { Nombre = "comisiones:manage", Modulo = "Comisiones", Descripcion = "Gestionar y recalcular todas las comisiones" },
                new Permiso { Nombre = "comisiones:create", Modulo = "Comisiones", Descripcion = "Crear comisiones manualmente" },
                new Permiso { Nombre = "comisiones:update", Modulo = "Comisiones", Descripcion = "Editar comisiones y ratios" },
                new Permiso { Nombre = "comisiones:delete", Modulo = "Comisiones", Descripcion = "Eliminar comisiones" },

                // Equipos
                new Permiso { Nombre = "equipos:manage", Modulo = "Equipos", Descripcion = "Gestionar equipos y jerarquías" },

                // Dashboard
                new Permiso { Nombre = "dashboard:admin", Modulo = "Dashboard", Descripcion = "Ver métricas globales de administrador" },

                // RRHH
                new Permiso { Nombre = "rrhh:view", Modulo = "RRHH", Descripcion = "Ver datos de personal" },
                new Permiso { Nombre = "rrhh:edit", Modulo = "RRHH", Descripcion = "Editar fichas de personal y salarios" }
            };

            foreach (var perm in permissions)
            {
                var existing = await context.Permisos.FirstOrDefaultAsync(p => p.Nombre == perm.Nombre);
                if (existing == null)
                {
                    context.Permisos.Add(perm);
                }
            }
            await context.SaveChangesAsync();

            // Auto-assign to Admin Role
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Nombre == "Admin");
            if (adminRole != null)
            {
                var allPermIds = await context.Permisos.Select(p => p.Id).ToListAsync();
                foreach (var pId in allPermIds)
                {
                    if (!await context.RolPermisos.AnyAsync(rp => rp.RolId == adminRole.Id && rp.PermisoId == pId))
                    {
                        context.RolPermisos.Add(new RolPermiso 
                        { 
                            RolId = adminRole.Id, 
                            PermisoId = pId,
                            FechaAsignacion = DateTime.UtcNow
                        });
                    }
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
