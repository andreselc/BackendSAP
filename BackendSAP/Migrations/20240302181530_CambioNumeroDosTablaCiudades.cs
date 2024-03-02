using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendSAP.Migrations
{
    /// <inheritdoc />
    public partial class CambioNumeroDosTablaCiudades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ciudades_Estados_EstadosId",
                table: "Ciudades");

            migrationBuilder.DropIndex(
                name: "IX_Ciudades_EstadosId",
                table: "Ciudades");

            migrationBuilder.DropColumn(
                name: "EstadosId",
                table: "Ciudades");

            migrationBuilder.RenameColumn(
                name: "estadoId",
                table: "Ciudades",
                newName: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_EstadoId",
                table: "Ciudades",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ciudades_Estados_EstadoId",
                table: "Ciudades",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ciudades_Estados_EstadoId",
                table: "Ciudades");

            migrationBuilder.DropIndex(
                name: "IX_Ciudades_EstadoId",
                table: "Ciudades");

            migrationBuilder.RenameColumn(
                name: "EstadoId",
                table: "Ciudades",
                newName: "estadoId");

            migrationBuilder.AddColumn<int>(
                name: "EstadosId",
                table: "Ciudades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_EstadosId",
                table: "Ciudades",
                column: "EstadosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ciudades_Estados_EstadosId",
                table: "Ciudades",
                column: "EstadosId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
