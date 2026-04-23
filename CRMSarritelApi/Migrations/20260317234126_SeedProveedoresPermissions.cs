using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProveedoresPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed proveedores permissions (idempotent)
            migrationBuilder.Sql(@"
                INSERT INTO ""Permisos"" (""Id"", ""Descripcion"", ""Modulo"", ""Nombre"") VALUES
                (37, 'Permiso para read en el módulo Proveedores', 'Proveedores', 'proveedores:read'),
                (38, 'Permiso para create en el módulo Proveedores', 'Proveedores', 'proveedores:create'),
                (39, 'Permiso para update en el módulo Proveedores', 'Proveedores', 'proveedores:update'),
                (40, 'Permiso para delete en el módulo Proveedores', 'Proveedores', 'proveedores:delete')
                ON CONFLICT (""Id"") DO NOTHING;
            ");

            // Assign all proveedores permissions to Admin role (Id=1)
            migrationBuilder.Sql(@"
                INSERT INTO ""RolPermisos"" (""PermisoId"", ""RolId"", ""FechaAsignacion"")
                SELECT p.""Id"", 1, NOW()
                FROM ""Permisos"" p
                WHERE p.""Modulo"" = 'Proveedores'
                  AND NOT EXISTS (
                    SELECT 1 FROM ""RolPermisos"" rp
                    WHERE rp.""PermisoId"" = p.""Id"" AND rp.""RolId"" = 1
                  );
            ");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHUX9t7zCI5rwdxoCM8bDkfpB4c+7RPdXXYItK3tLJrl4wcQBRXc+4SMwVwst56cbA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM ""RolPermisos"" WHERE ""PermisoId"" IN (37, 38, 39, 40);
                DELETE FROM ""Permisos"" WHERE ""Id"" IN (37, 38, 39, 40);
            ");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMMZfGES2dOph5Wb9PQIZBPzftMSmEv2CITrw1IDLzP/LOdYUTRoGsIIdVYJ/3PDXQ==");
        }
    }
}
