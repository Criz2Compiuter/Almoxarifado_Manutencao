using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Almoxarifado_Mirvi.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Produto",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Produto");
        }
    }
}
