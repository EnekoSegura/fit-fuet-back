using Microsoft.EntityFrameworkCore.Migrations;

namespace fitfuet.back.Migrations
{
    public partial class crearUsuarioModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Dni = table.Column<string>(type: "varchar(9)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(20)", nullable: false),
                    Apellido = table.Column<string>(type: "varchar(20)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Passwd = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Dni);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
