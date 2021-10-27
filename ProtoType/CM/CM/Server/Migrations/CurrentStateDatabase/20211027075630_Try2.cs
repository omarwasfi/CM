using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Server.Migrations.CurrentStateDatabase
{
    public partial class Try2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCars_AspNetUsers_PersonDataModelId",
                table: "OwnedCars");

            migrationBuilder.RenameColumn(
                name: "PersonDataModelId",
                table: "OwnedCars",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedCars_PersonDataModelId",
                table: "OwnedCars",
                newName: "IX_OwnedCars_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCars_AspNetUsers_PersonId",
                table: "OwnedCars",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCars_AspNetUsers_PersonId",
                table: "OwnedCars");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "OwnedCars",
                newName: "PersonDataModelId");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedCars_PersonId",
                table: "OwnedCars",
                newName: "IX_OwnedCars_PersonDataModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCars_AspNetUsers_PersonDataModelId",
                table: "OwnedCars",
                column: "PersonDataModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
