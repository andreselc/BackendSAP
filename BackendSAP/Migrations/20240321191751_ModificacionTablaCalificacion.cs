using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendSAP.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionTablaCalificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_usuarioId",
                table: "Calificaciones");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_usuarioId_psicologoId",
                table: "Calificaciones",
                columns: new[] { "usuarioId", "psicologoId" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Usuario_Psicologo_Calificacion",
                table: "Calificaciones",
                sql: "usuarioId <> psicologoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_usuarioId_psicologoId",
                table: "Calificaciones");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Usuario_Psicologo_Calificacion",
                table: "Calificaciones");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_usuarioId",
                table: "Calificaciones",
                column: "usuarioId");
        }
    }
}
