using Microsoft.EntityFrameworkCore.Migrations;

namespace fitfuet.back.Migrations
{
    public partial class modeloEjercicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ejercicio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    MusculoEjercitado = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Explicacion = table.Column<string>(nullable: true),
                    Met = table.Column<float>(nullable: true),
                    Imagen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejercicio", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ejercicio");
        }
    }
}
