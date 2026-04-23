using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFinanzasPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""Permisos"" (""Id"", ""Descripcion"", ""Modulo"", ""Nombre"")
                VALUES (37, 'Permiso para ver finanzas en ventas', 'Ventas', 'ventas:view_finances')
                ON CONFLICT (""Id"") DO NOTHING;
            ");
            
            migrationBuilder.Sql(@"
                INSERT INTO ""RolPermisos"" (""PermisoId"", ""RolId"", ""FechaAsignacion"")
                SELECT 37, 1, NOW()
                WHERE NOT EXISTS (
                    SELECT 1 FROM ""RolPermisos"" WHERE ""PermisoId"" = 37 AND ""RolId"" = 1
                );
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM ""RolPermisos"" WHERE ""PermisoId"" = 37;");
            migrationBuilder.Sql(@"DELETE FROM ""Permisos"" WHERE ""Id"" = 37;");
        }
    }
}
