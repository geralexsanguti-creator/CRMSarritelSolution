using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comisiones_Comerciales_ComercialId",
                table: "Comisiones");

            migrationBuilder.DropIndex(
                name: "IX_Comisiones_ComercialId",
                table: "Comisiones");

            migrationBuilder.DropColumn(
                name: "ComercialId",
                table: "Comisiones");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIcaXVk77WD/2ATx10wSYuSpKT72y8o36o2wwPzGQQxyOrYjcM21KBK4rMlW+KtetQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComercialId",
                table: "Comisiones",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "2a11W7ga9jCrr7zuekSpI6ucdOPC1Ryk7iR/yYiiJP4/Ylk.EY6v2SNDy");

            migrationBuilder.CreateIndex(
                name: "IX_Comisiones_ComercialId",
                table: "Comisiones",
                column: "ComercialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comisiones_Comerciales_ComercialId",
                table: "Comisiones",
                column: "ComercialId",
                principalTable: "Comerciales",
                principalColumn: "Id");
        }
    }
}
