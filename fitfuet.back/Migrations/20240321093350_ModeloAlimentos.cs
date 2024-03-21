using Microsoft.EntityFrameworkCore.Migrations;

namespace fitfuet.back.Migrations
{
    public partial class ModeloAlimentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alimentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    TipoAlimento = table.Column<string>(type: "varchar(100)", nullable: false),
                    Calorias = table.Column<float>(nullable: false),
                    Carbohidratos = table.Column<float>(nullable: false),
                    Proteinas = table.Column<float>(nullable: false),
                    Grasas = table.Column<float>(nullable: false),
                    Fibra = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alimentos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alimentos");

        }
    }
}
