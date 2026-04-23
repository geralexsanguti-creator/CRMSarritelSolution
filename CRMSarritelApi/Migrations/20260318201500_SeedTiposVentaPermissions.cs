using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedTiposVentaPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed tipos-venta permissions (idempotent)
            migrationBuilder.Sql(@"
                INSERT INTO ""Permisos"" (""Id"", ""Descripcion"", ""Modulo"", ""Nombre"") VALUES
                (41, 'Permiso para read en el módulo Tipos de Venta', 'Tipos de Venta', 'tipos-venta:read'),
                (42, 'Permiso para create en el módulo Tipos de Venta', 'Tipos de Venta', 'tipos-venta:create'),
                (43, 'Permiso para update en el módulo Tipos de Venta', 'Tipos de Venta', 'tipos-venta:update'),
                (44, 'Permiso para delete en el módulo Tipos de Venta', 'Tipos de Venta', 'tipos-venta:delete')
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Assign all tipos-venta permissions to Admin role (Id=1)
            migrationBuilder.Sql(@"
                INSERT INTO ""RolPermisos"" (""PermisoId"", ""RolId"", ""FechaAsignacion"")
                SELECT p.""Id"", 1, NOW()
                FROM ""Permisos"" p
                WHERE p.""Modulo"" = 'Tipos de Venta'
                  AND NOT EXISTS (
                    SELECT 1 FROM ""RolPermisos"" rp
                    WHERE rp.""PermisoId"" = p.""Id"" AND rp.""RolId"" = 1
                  );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM ""RolPermisos"" WHERE ""PermisoId"" IN (41, 42, 43, 44);
                DELETE FROM ""Permisos"" WHERE ""Id"" IN (41, 42, 43, 44);
            ");
        }
    }
}
