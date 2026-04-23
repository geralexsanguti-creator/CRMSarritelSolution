using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPausasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pausas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FichajeId = table.Column<int>(type: "integer", nullable: false),
                    HoraInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Motivo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pausas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pausas_Fichajes_FichajeId",
                        column: x => x.FichajeId,
                        principalTable: "Fichajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBG8ILZq1HudIJQTKfqd8CERr4YezHl4+gGmGRE7T55iCh9zAMFg4nXq7J935mu5JA==");

            migrationBuilder.CreateIndex(
                name: "IX_Pausas_FichajeId",
                table: "Pausas",
                column: "FichajeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pausas");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEERJNRH4INXzufUzbttLkSukOkSGXTnPMvwPEq3AbF/h9ynaLGwlVEl+EQLud8Mvuw==");
        }
    }
}
