using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Almoxarifado_Mirvi.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Almoxarifado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almoxarifado", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Corredor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AlmoxarifadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corredor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corredor_Almoxarifado_AlmoxarifadoId",
                        column: x => x.AlmoxarifadoId,
                        principalTable: "Almoxarifado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Maquina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AlmoxarifadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maquina_Almoxarifado_AlmoxarifadoId",
                        column: x => x.AlmoxarifadoId,
                        principalTable: "Almoxarifado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Repartição",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AlmoxarifadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repartição", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repartição_Almoxarifado_AlmoxarifadoId",
                        column: x => x.AlmoxarifadoId,
                        principalTable: "Almoxarifado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prateleira",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CorredorId = table.Column<int>(type: "int", nullable: false),
                    AlmoxarifadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prateleira", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prateleira_Almoxarifado_AlmoxarifadoId",
                        column: x => x.AlmoxarifadoId,
                        principalTable: "Almoxarifado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prateleira_Corredor_CorredorId",
                        column: x => x.CorredorId,
                        principalTable: "Corredor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrateleirasId = table.Column<int>(type: "int", nullable: true),
                    CorredorId = table.Column<int>(type: "int", nullable: true),
                    AlmoxarifadoId = table.Column<int>(type: "int", nullable: true),
                    RepartiçãoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Almoxarifado_AlmoxarifadoId",
                        column: x => x.AlmoxarifadoId,
                        principalTable: "Almoxarifado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Endereco_Corredor_CorredorId",
                        column: x => x.CorredorId,
                        principalTable: "Corredor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Endereco_Prateleira_PrateleirasId",
                        column: x => x.PrateleirasId,
                        principalTable: "Prateleira",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Endereco_Repartição_RepartiçãoId",
                        column: x => x.RepartiçãoId,
                        principalTable: "Repartição",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EnderecosId = table.Column<int>(type: "int", nullable: true),
                    PrateleirasId = table.Column<int>(type: "int", nullable: true),
                    CorredorId = table.Column<int>(type: "int", nullable: true),
                    RepartiçãoId = table.Column<int>(type: "int", nullable: true),
                    MaquinaId = table.Column<int>(type: "int", nullable: true),
                    AlmoxarifadoId = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Categoria = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Minimo = table.Column<int>(type: "int", nullable: true),
                    Maximo = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CodigoDeCompra = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Local = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Linha = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    C_STalisca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hpn = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Referencia = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    H225_H300 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fornecedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diametro = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comprimento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Conexao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Medida = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fabricante = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    S_N = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    QuantidadeTotalIntalada = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Almoxarifado_AlmoxarifadoId",
                        column: x => x.AlmoxarifadoId,
                        principalTable: "Almoxarifado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_Corredor_CorredorId",
                        column: x => x.CorredorId,
                        principalTable: "Corredor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_Endereco_EnderecosId",
                        column: x => x.EnderecosId,
                        principalTable: "Endereco",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_Maquina_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquina",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_Prateleira_PrateleirasId",
                        column: x => x.PrateleirasId,
                        principalTable: "Prateleira",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_Repartição_RepartiçãoId",
                        column: x => x.RepartiçãoId,
                        principalTable: "Repartição",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Corredor_AlmoxarifadoId",
                table: "Corredor",
                column: "AlmoxarifadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_AlmoxarifadoId",
                table: "Endereco",
                column: "AlmoxarifadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_CorredorId",
                table: "Endereco",
                column: "CorredorId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_PrateleirasId",
                table: "Endereco",
                column: "PrateleirasId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_RepartiçãoId",
                table: "Endereco",
                column: "RepartiçãoId");

            migrationBuilder.CreateIndex(
                name: "IX_Maquina_AlmoxarifadoId",
                table: "Maquina",
                column: "AlmoxarifadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Prateleira_AlmoxarifadoId",
                table: "Prateleira",
                column: "AlmoxarifadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Prateleira_CorredorId",
                table: "Prateleira",
                column: "CorredorId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_AlmoxarifadoId",
                table: "Produto",
                column: "AlmoxarifadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CorredorId",
                table: "Produto",
                column: "CorredorId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_EnderecosId",
                table: "Produto",
                column: "EnderecosId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_MaquinaId",
                table: "Produto",
                column: "MaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_PrateleirasId",
                table: "Produto",
                column: "PrateleirasId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_RepartiçãoId",
                table: "Produto",
                column: "RepartiçãoId");

            migrationBuilder.CreateIndex(
                name: "IX_Repartição_AlmoxarifadoId",
                table: "Repartição",
                column: "AlmoxarifadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Maquina");

            migrationBuilder.DropTable(
                name: "Prateleira");

            migrationBuilder.DropTable(
                name: "Repartição");

            migrationBuilder.DropTable(
                name: "Corredor");

            migrationBuilder.DropTable(
                name: "Almoxarifado");
        }
    }
}
