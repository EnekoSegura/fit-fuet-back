using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace fitfuet.back.Migrations
{
    public partial class cuentaActivaUsuarioYModeloDatosUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuentaActiva",
                table: "Usuario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DatosUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(nullable: false),
                    Peso = table.Column<float>(nullable: false),
                    Altura = table.Column<float>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    RegistroActivo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatosUsuario_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatosUsuario_IdUsuario",
                table: "DatosUsuario",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosUsuario");

            migrationBuilder.DropColumn(
                name: "CuentaActiva",
                table: "Usuario");
        }
    }
}
