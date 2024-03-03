using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendSAP.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaTrastornoEInterseccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrastornoPsicologico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Causas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sintomas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrastornoPsicologico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EspecialidadPsicologo",
                columns: table => new
                {
                    psicologoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    trastornoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadPsicologo", x => new { x.psicologoId, x.trastornoId });
                    table.ForeignKey(
                        name: "FK_EspecialidadPsicologo_AspNetUsers_psicologoId",
                        column: x => x.psicologoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspecialidadPsicologo_TrastornoPsicologico_trastornoId",
                        column: x => x.trastornoId,
                        principalTable: "TrastornoPsicologico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadPsicologo_trastornoId",
                table: "EspecialidadPsicologo",
                column: "trastornoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EspecialidadPsicologo");

            migrationBuilder.DropTable(
                name: "TrastornoPsicologico");
        }
    }
}
