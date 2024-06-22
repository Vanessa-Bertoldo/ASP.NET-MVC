using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.students.Migrations
{
    /// <inheritdoc />
    public partial class addNewsMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    FornecedorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.FornecedorId);
                });

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    LojaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.LojaId);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Preco = table.Column<decimal>(type: "NUMBER(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FornecedorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produtos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "FornecedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataPedido = table.Column<DateTime>(type: "DATE", nullable: false),
                    ClienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LojaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoId);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Lojas_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Lojas",
                        principalColumn: "LojaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoProdutoModel",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProdutoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ClienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LojaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PedidoProdutoModelPedidoId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    PedidoProdutoModelProdutoId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoProdutoModel", x => new { x.PedidoId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_PedidoProdutoModel_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProdutoModel_Lojas_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Lojas",
                        principalColumn: "LojaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProdutoModel_PedidoProdutoModel_PedidoProdutoModelPedidoId_PedidoProdutoModelProdutoId",
                        columns: x => new { x.PedidoProdutoModelPedidoId, x.PedidoProdutoModelProdutoId },
                        principalTable: "PedidoProdutoModel",
                        principalColumns: new[] { "PedidoId", "ProdutoId" });
                    table.ForeignKey(
                        name: "FK_PedidoProdutoModel_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProdutoModel_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProdutoModel_ClienteId",
                table: "PedidoProdutoModel",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProdutoModel_LojaId",
                table: "PedidoProdutoModel",
                column: "LojaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProdutoModel_PedidoProdutoModelPedidoId_PedidoProdutoModelProdutoId",
                table: "PedidoProdutoModel",
                columns: new[] { "PedidoProdutoModelPedidoId", "PedidoProdutoModelProdutoId" });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProdutoModel_ProdutoId",
                table: "PedidoProdutoModel",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_LojaId",
                table: "Pedidos",
                column: "LojaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
                column: "FornecedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoProdutoModel");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropTable(
                name: "Fornecedores");
        }
    }
}
