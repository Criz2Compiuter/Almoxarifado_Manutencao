using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Almoxarifado_Mirvi.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartHeaders_CartHeaderId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartHeaders",
                table: "CartHeaders");

            migrationBuilder.RenameTable(
                name: "CartHeaders",
                newName: "CartHeader");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "CartItems",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "CartItems",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartHeader",
                table: "CartHeader",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartHeader_CartHeaderId",
                table: "CartItems",
                column: "CartHeaderId",
                principalTable: "CartHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartHeader_CartHeaderId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartHeader",
                table: "CartHeader");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "CartHeader",
                newName: "CartHeaders");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartHeaders",
                table: "CartHeaders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartHeaders_CartHeaderId",
                table: "CartItems",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
