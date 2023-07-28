using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Almoxarifado_Mirvi.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prateleira_Almoxarifado_AlmoxarifadoId",
                table: "Prateleira");

            migrationBuilder.AlterColumn<int>(
                name: "AlmoxarifadoId",
                table: "Prateleira",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Prateleira_Almoxarifado_AlmoxarifadoId",
                table: "Prateleira",
                column: "AlmoxarifadoId",
                principalTable: "Almoxarifado",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prateleira_Almoxarifado_AlmoxarifadoId",
                table: "Prateleira");

            migrationBuilder.AlterColumn<int>(
                name: "AlmoxarifadoId",
                table: "Prateleira",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prateleira_Almoxarifado_AlmoxarifadoId",
                table: "Prateleira",
                column: "AlmoxarifadoId",
                principalTable: "Almoxarifado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
