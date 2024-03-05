using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendSAP.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaCalificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    puntaje = table.Column<int>(type: "int", nullable: false),
                    usuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    psicologoId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_AspNetUsers_psicologoId",
                        column: x => x.psicologoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calificaciones_AspNetUsers_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_psicologoId",
                table: "Calificaciones",
                column: "psicologoId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_usuarioId",
                table: "Calificaciones",
                column: "usuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");
        }
    }
}
