using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendSAP.Migrations
{
    /// <inheritdoc />
    public partial class CambioDosTablaCalificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_puntaje",
                table: "Calificaciones",
                sql: "[puntaje]  IN (1, 2, 3, 4, 5)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_puntaje",
                table: "Calificaciones");
        }
    }
}
