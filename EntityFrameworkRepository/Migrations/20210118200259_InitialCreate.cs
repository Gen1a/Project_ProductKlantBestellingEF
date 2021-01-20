using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkRepository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klant",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AUTO_TIME_CREATION = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    AUTO_UPDATE_COUNT = table.Column<long>(type: "bigint", nullable: false),
                    AUTO_TIME_UPDATE = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    AUTO_UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(suser_sname())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VALID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    AUTO_TIME_CREATION = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    AUTO_UPDATE_COUNT = table.Column<long>(type: "bigint", nullable: false),
                    AUTO_TIME_UPDATE = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    AUTO_UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(suser_sname())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bestelling",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantId = table.Column<long>(type: "bigint", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Betaald = table.Column<bool>(type: "bit", nullable: false),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AUTO_TIME_CREATION = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    AUTO_UPDATE_COUNT = table.Column<long>(type: "bigint", nullable: false),
                    AUTO_TIME_UPDATE = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysdatetime())"),
                    AUTO_UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(suser_sname())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestelling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bestelling_Klant",
                        column: x => x.KlantId,
                        principalTable: "Klant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Bestelling",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    BestellingId = table.Column<long>(type: "bigint", nullable: false),
                    Aantal = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AutoTimeCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutoUpdateCount = table.Column<long>(type: "bigint", nullable: false),
                    AutoTimeUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutoUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Bestelling", x => new { x.ProductId, x.BestellingId });
                    table.ForeignKey(
                        name: "FK_Product_Bestelling_Bestelling",
                        column: x => x.BestellingId,
                        principalTable: "Bestelling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Bestelling_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bestelling_KlantId",
                table: "Bestelling",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Bestelling_BestellingId",
                table: "Product_Bestelling",
                column: "BestellingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_Bestelling");

            migrationBuilder.DropTable(
                name: "Bestelling");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Klant");
        }
    }
}
