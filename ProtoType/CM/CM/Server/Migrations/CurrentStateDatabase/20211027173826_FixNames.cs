using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Server.Migrations.CurrentStateDatabase
{
    public partial class FixNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_OwnedCars_OwnedCarId",
                table: "Problems");

            migrationBuilder.RenameColumn(
                name: "Descrition",
                table: "Cars",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "OwnedCarId",
                table: "Problems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarId",
                table: "OwnedCars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OwnedCars_CarId",
                table: "OwnedCars",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCars_Cars_CarId",
                table: "OwnedCars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_OwnedCars_OwnedCarId",
                table: "Problems",
                column: "OwnedCarId",
                principalTable: "OwnedCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCars_Cars_CarId",
                table: "OwnedCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Problems_OwnedCars_OwnedCarId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_OwnedCars_CarId",
                table: "OwnedCars");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "OwnedCars");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Cars",
                newName: "Descrition");

            migrationBuilder.AlterColumn<string>(
                name: "OwnedCarId",
                table: "Problems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_OwnedCars_OwnedCarId",
                table: "Problems",
                column: "OwnedCarId",
                principalTable: "OwnedCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
