using Microsoft.EntityFrameworkCore.Migrations;

namespace fitfuet.back.Migrations
{
    public partial class ModeloDietaCorregirFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dieta_Alimentos_IdEjercicio",
                table: "Dieta");

            migrationBuilder.DropIndex(
                name: "IX_Dieta_IdEjercicio",
                table: "Dieta");

            migrationBuilder.DropColumn(
                name: "IdEjercicio",
                table: "Dieta");

            migrationBuilder.CreateIndex(
                name: "IX_Dieta_IdAlimento",
                table: "Dieta",
                column: "IdAlimento");

            migrationBuilder.AddForeignKey(
                name: "FK_Dieta_Alimentos_IdAlimento",
                table: "Dieta",
                column: "IdAlimento",
                principalTable: "Alimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dieta_Alimentos_IdAlimento",
                table: "Dieta");

            migrationBuilder.DropIndex(
                name: "IX_Dieta_IdAlimento",
                table: "Dieta");

            migrationBuilder.AddColumn<int>(
                name: "IdEjercicio",
                table: "Dieta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dieta_IdEjercicio",
                table: "Dieta",
                column: "IdEjercicio");

            migrationBuilder.AddForeignKey(
                name: "FK_Dieta_Alimentos_IdEjercicio",
                table: "Dieta",
                column: "IdEjercicio",
                principalTable: "Alimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
