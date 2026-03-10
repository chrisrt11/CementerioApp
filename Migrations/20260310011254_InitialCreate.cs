using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CementerioApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sector = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Fila = table.Column<int>(type: "INTEGER", nullable: false),
                    Columna = table.Column<int>(type: "INTEGER", nullable: false),
                    Latitud = table.Column<double>(type: "REAL", nullable: false),
                    Longitud = table.Column<double>(type: "REAL", nullable: false),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Difuntos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Cedula = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaFallecimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Observaciones = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    LoteId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Difuntos_Lotes_LoteId",
                        column: x => x.LoteId,
                        principalTable: "Lotes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Lotes",
                columns: new[] { "Id", "Columna", "Descripcion", "Estado", "Fila", "Latitud", "Longitud", "Sector" },
                values: new object[,]
                {
                    { 1, 1, "Lote sector A, fila 1, columna 1", 1, 1, -34.603700000000003, -58.381599999999999, "A" },
                    { 2, 2, "Lote sector A, fila 1, columna 2", 1, 1, -34.6038, -58.381700000000002, "A" },
                    { 3, 3, "Lote sector A, fila 1, columna 3", 0, 1, -34.603900000000003, -58.381799999999998, "A" },
                    { 4, 1, "Lote sector B, fila 1, columna 1", 0, 1, -34.603999999999999, -58.381900000000002, "B" },
                    { 5, 2, "Lote sector B, fila 1, columna 2", 1, 1, -34.604100000000003, -58.381999999999998, "B" },
                    { 6, 3, "Lote sector B, fila 1, columna 3", 0, 1, -34.604199999999999, -58.382100000000001, "B" },
                    { 7, 1, "Lote sector C, fila 1, columna 1", 0, 1, -34.604300000000002, -58.382199999999997, "C" },
                    { 8, 2, "Lote sector C, fila 1, columna 2", 0, 1, -34.604399999999998, -58.382300000000001, "C" },
                    { 9, 3, "Lote sector C, fila 1, columna 3", 1, 1, -34.604500000000002, -58.382399999999997, "C" }
                });

            migrationBuilder.InsertData(
                table: "Difuntos",
                columns: new[] { "Id", "Apellido", "Cedula", "FechaFallecimiento", "FechaNacimiento", "LoteId", "Nombre", "Observaciones" },
                values: new object[,]
                {
                    { 1, "García", "12345678", new DateTime(2010, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1940, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Juan", "Descansa en paz" },
                    { 2, "López", "87654321", new DateTime(2018, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1955, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "María", "Siempre en nuestros corazones" },
                    { 3, "Rodríguez", "11223344", new DateTime(2020, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1930, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Carlos", "En memoria eterna" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Difuntos_LoteId",
                table: "Difuntos",
                column: "LoteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Difuntos");

            migrationBuilder.DropTable(
                name: "Lotes");
        }
    }
}
