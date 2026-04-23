using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRMSarritelApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorialVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VentaId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true),
                    FechaCambio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Accion = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    CampoModificado = table.Column<string>(type: "text", nullable: true),
                    ValorAnterior = table.Column<string>(type: "text", nullable: true),
                    ValorNuevo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialVentas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_HistorialVentas_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1000, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(5392));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1001, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6480));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1002, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6483));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1003, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6487));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1004, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6489));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1005, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6491));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1006, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6493));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1007, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6496));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1008, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6498));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1009, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6499));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1010, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6501));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1011, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6504));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1012, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6506));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1013, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6509));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1014, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6511));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1015, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6515));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1016, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6517));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1017, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6519));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1018, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6520));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1019, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6522));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1020, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6523));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1021, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6524));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1022, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6526));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1023, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6527));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1024, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6529));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1025, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6530));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1026, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6532));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1027, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6563));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1028, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1029, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6568));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1030, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6571));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1031, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6572));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1032, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6574));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1033, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6576));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1034, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6577));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1035, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6579));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1036, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6581));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1037, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6583));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1038, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6586));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1039, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6587));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1040, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6590));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1041, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6592));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1042, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6594));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1043, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6595));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1044, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6597));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1045, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6599));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1046, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6602));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1047, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6604));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1048, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6606));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1049, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6608));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1050, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6611));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1051, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6613));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1052, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6615));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1053, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6617));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1054, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6619));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1055, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6621));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1056, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 18, 13, 16, 687, DateTimeKind.Utc).AddTicks(6624));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIGgmVyKMQasyDZqkiTwZ+bdXwfmvLs1hr1qvn2Bee2n9mQtFQRenBiIgcRCVZQ8oQ==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGJXsp5F89xAzFrCN5igcyMjM1TmLFX3HRcjJ7nVAzVnmQMsSD2lg6UMSkas9HLLjw==");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialVentas_UsuarioId",
                table: "HistorialVentas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialVentas_VentaId",
                table: "HistorialVentas",
                column: "VentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialVentas");

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1000, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1001, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8048));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1002, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1003, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1004, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1005, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8052));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1006, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8052));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1007, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8053));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1008, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1009, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1010, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8055));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1011, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1012, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8056));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1013, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1014, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1015, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1016, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8061));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1017, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1018, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1019, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1020, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1021, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1022, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1023, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1024, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1025, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8068));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1026, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8068));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1027, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8069));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1028, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1029, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1030, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8071));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1031, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8072));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1032, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8073));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1033, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8073));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1034, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8074));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1035, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8075));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1036, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8076));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1037, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8076));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1038, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1039, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8078));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1040, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8079));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1041, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8081));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1042, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1043, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1044, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1045, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1046, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1047, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8087));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1048, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1049, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1050, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8089));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1051, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1052, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1053, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1054, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1055, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "RolPermisos",
                keyColumns: new[] { "PermisoId", "RolId" },
                keyValues: new object[] { 1056, 1 },
                column: "FechaAsignacion",
                value: new DateTime(2026, 4, 22, 12, 17, 52, 945, DateTimeKind.Utc).AddTicks(8093));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPegp8dHLFaICD1Sqyn4vr8lqhXw4I3LWe4mJnFoGPA39yyFQ0+UmCuj0fOj/56a0Q==");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEG94S1/yoVC/PqC1Mw7u/2q3xw5X6ByTmfO0yWBelS/ScGQJBpHJ6S9cSUkU1z0m5g==");
        }
    }
}
