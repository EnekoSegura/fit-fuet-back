using Microsoft.EntityFrameworkCore.Migrations;

namespace fitfuet.back.Migrations
{
    public partial class fotoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Usuario",
                type: "nvarchar(MAX)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Usuario");
        }
    }
}
