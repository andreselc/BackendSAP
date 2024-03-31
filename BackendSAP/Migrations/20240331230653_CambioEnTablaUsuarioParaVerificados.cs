using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendSAP.Migrations
{
    /// <inheritdoc />
    public partial class CambioEnTablaUsuarioParaVerificados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Verificado",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_EstadoVerificado",
                table: "AspNetUsers",
                sql: "[Verificado] IN ('V', 'F')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_EstadoVerificado",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Verificado",
                table: "AspNetUsers");
        }
    }
}
