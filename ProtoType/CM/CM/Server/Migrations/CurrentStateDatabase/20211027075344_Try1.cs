using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Server.Migrations.CurrentStateDatabase
{
    public partial class Try1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnedCarId",
                table: "Problems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "Problems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_OwnedCarId",
                table: "Problems",
                column: "OwnedCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_PersonId",
                table: "Problems",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_AspNetUsers_PersonId",
                table: "Problems",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_OwnedCars_OwnedCarId",
                table: "Problems",
                column: "OwnedCarId",
                principalTable: "OwnedCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_AspNetUsers_PersonId",
                table: "Problems");

            migrationBuilder.DropForeignKey(
                name: "FK_Problems_OwnedCars_OwnedCarId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_OwnedCarId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_PersonId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "OwnedCarId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Problems");
        }
    }
}
