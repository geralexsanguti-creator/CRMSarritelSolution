using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReconciledSystemV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReparticionesComision_Roles_RolId",
                table: "ReparticionesComision");

            migrationBuilder.DropTable(
                name: "ProductoReglas");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "ReparticionesComision",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "EquipoId",
                table: "ReparticionesComision",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorVenta",
                table: "ReglasComisiones",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "JefeEquipoId",
                table: "Equipos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarpetasReglas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    ProveedorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarpetasReglas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarpetasReglas_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CarpetaReglasComision",
                columns: table => new
                {
                    CarpetaReglasId = table.Column<int>(type: "integer", nullable: false),
                    ReglaComisionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarpetaReglasComision", x => new { x.CarpetaReglasId, x.ReglaComisionId });
                    table.ForeignKey(
                        name: "FK_CarpetaReglasComision_CarpetasReglas_CarpetaReglasId",
                        column: x => x.CarpetaReglasId,
                        principalTable: "CarpetasReglas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarpetaReglasComision_ReglasComisiones_ReglaComisionId",
                        column: x => x.ReglaComisionId,
                        principalTable: "ReglasComisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoCarpetas",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "integer", nullable: false),
                    CarpetaReglasId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoCarpetas", x => new { x.ProductoId, x.CarpetaReglasId });
                    table.ForeignKey(
                        name: "FK_ProductoCarpetas_CarpetasReglas_CarpetaReglasId",
                        column: x => x.CarpetaReglasId,
                        principalTable: "CarpetasReglas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoCarpetas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(3158));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4129));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4135));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4137));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4139));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4141));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4145));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4148));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4149));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4151));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4152));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4155));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4156));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4157));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4158));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4161));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4162));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4164));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4165));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4168));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4169));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4171));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4172));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4174));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4180));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4181));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4197));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4200));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4201));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4205));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4206));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4207));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4208));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4210));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 5, 16, 18, 51, 661, DateTimeKind.Utc).AddTicks(4212));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEODyPUr/laH7KEeziRIn9A8YIPSkHngZPQJM1oM28dIU0NYm3E5yYTsAOpait71Ufg==");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Activo", "Comisiones", "Departamento", "Email", "FechaContratacion", "FechaCreation", "Nombre", "PasswordHash", "Puesto", "RefreshToken", "RefreshTokenExpiryTime", "SalarioBase", "SuperiorId", "Username" },
                values: new object[] { 99, true, null, null, "sistema@crmsarritel.com", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "SISTEMA (Organización)", "AQAAAAIAAYagAAAAEOxRb+cfIXrfK9Dfo/leKSmFjLfze7y6C9zssLcb+WJslANBlhLqbFhtci2KvyzvwA==", null, null, null, null, null, "sistema" });

            migrationBuilder.InsertData(
                table: "UsuarioRoles",
                columns: new[] { "RolId", "UsuarioId", "FechaAsignacion" },
                values: new object[] { 1, 99, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_ReparticionesComision_EquipoId",
                table: "ReparticionesComision",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_JefeEquipoId",
                table: "Equipos",
                column: "JefeEquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarpetaReglasComision_ReglaComisionId",
                table: "CarpetaReglasComision",
                column: "ReglaComisionId");

            migrationBuilder.CreateIndex(
                name: "IX_CarpetasReglas_ProveedorId",
                table: "CarpetasReglas",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCarpetas_CarpetaReglasId",
                table: "ProductoCarpetas",
                column: "CarpetaReglasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Usuarios_JefeEquipoId",
                table: "Equipos",
                column: "JefeEquipoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ReparticionesComision_Equipos_EquipoId",
                table: "ReparticionesComision",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ReparticionesComision_Roles_RolId",
                table: "ReparticionesComision",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Usuarios_JefeEquipoId",
                table: "Equipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ReparticionesComision_Equipos_EquipoId",
                table: "ReparticionesComision");

            migrationBuilder.DropForeignKey(
                name: "FK_ReparticionesComision_Roles_RolId",
                table: "ReparticionesComision");

            migrationBuilder.DropTable(
                name: "CarpetaReglasComision");

            migrationBuilder.DropTable(
                name: "ProductoCarpetas");

            migrationBuilder.DropTable(
                name: "CarpetasReglas");

            migrationBuilder.DropIndex(
                name: "IX_ReparticionesComision_EquipoId",
                table: "ReparticionesComision");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_JefeEquipoId",
                table: "Equipos");

            migrationBuilder.DeleteData(
                table: "UsuarioRoles",
                keyColumns: new[] { "RolId", "UsuarioId" },
                keyValues: new object[] { 1, 99 });

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DropColumn(
                name: "EquipoId",
                table: "ReparticionesComision");

            migrationBuilder.DropColumn(
                name: "ValorVenta",
                table: "ReglasComisiones");

            migrationBuilder.DropColumn(
                name: "JefeEquipoId",
                table: "Equipos");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "ReparticionesComision",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductoReglas",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "integer", nullable: false),
                    ReglaComisionId = table.Column<int>(type: "integer", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoReglas", x => new { x.ProductoId, x.ReglaComisionId });
                    table.ForeignKey(
                        name: "FK_ProductoReglas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoReglas_ReglasComisiones_ReglaComisionId",
                        column: x => x.ReglaComisionId,
                        principalTable: "ReglasComisiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(770));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 2, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2277));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 3, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2285));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 4, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2287));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 5, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2289));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 6, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2291));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 7, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2293));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 8, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2294));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 9, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2297));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 10, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2298));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 11, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2302));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 12, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2303));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 13, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2305));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 14, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2306));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 15, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2307));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 16, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2309));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 17, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2311));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 18, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2313));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 19, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2316));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 20, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2317));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 21, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2318));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 22, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2319));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 23, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2321));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 24, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2322));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 25, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2324));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 26, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2325));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 27, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 28, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2327));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 29, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2329));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 30, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2330));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 31, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2331));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 32, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2333));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 33, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2335));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 34, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2336));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 35, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2337));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 36, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 1, 18, 30, 27, 66, DateTimeKind.Utc).AddTicks(2339));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEA0p+sF1vpuXNw2+WErqGUJlkpy9rHFeTn9FhZZjaA11lFQF579tRJZ8sQYYB3pFyQ==");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoReglas_ReglaComisionId",
                table: "ProductoReglas",
                column: "ReglaComisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReparticionesComision_Roles_RolId",
                table: "ReparticionesComision",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
