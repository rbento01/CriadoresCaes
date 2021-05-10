using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CriadoresCaes_tA_B.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Criadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeComercial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telemovel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Racas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNasc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LOP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RacaFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caes_Racas_RacaFK",
                        column: x => x.RacaFK,
                        principalTable: "Racas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CriadoresCaes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaesFK = table.Column<int>(type: "int", nullable: false),
                    CriadorFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriadoresCaes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriadoresCaes_Caes_CaesFK",
                        column: x => x.CaesFK,
                        principalTable: "Caes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriadoresCaes_Criadores_CriadorFK",
                        column: x => x.CriadorFK,
                        principalTable: "Criadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fotografias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fotografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFoto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaoFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotografias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fotografias_Caes_CaoFK",
                        column: x => x.CaoFK,
                        principalTable: "Caes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caes_RacaFK",
                table: "Caes",
                column: "RacaFK");

            migrationBuilder.CreateIndex(
                name: "IX_CriadoresCaes_CaesFK",
                table: "CriadoresCaes",
                column: "CaesFK");

            migrationBuilder.CreateIndex(
                name: "IX_CriadoresCaes_CriadorFK",
                table: "CriadoresCaes",
                column: "CriadorFK");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografias_CaoFK",
                table: "Fotografias",
                column: "CaoFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriadoresCaes");

            migrationBuilder.DropTable(
                name: "Fotografias");

            migrationBuilder.DropTable(
                name: "Criadores");

            migrationBuilder.DropTable(
                name: "Caes");

            migrationBuilder.DropTable(
                name: "Racas");
        }
    }
}
