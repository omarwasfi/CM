using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Server.Migrations.CurrentStateDatabase
{
    public partial class CompleteTheCurrentStateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCarDataModel_AspNetUsers_PersonDataModelId",
                table: "OwnedCarDataModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnedCarDataModel",
                table: "OwnedCarDataModel");

            migrationBuilder.RenameTable(
                name: "OwnedCarDataModel",
                newName: "OwnedCars");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedCarDataModel_PersonDataModelId",
                table: "OwnedCars",
                newName: "IX_OwnedCars_PersonDataModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnedCars",
                table: "OwnedCars",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Descrition = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CarBrandId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarBrands_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProblemTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Problems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProblemTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problems_ProblemTypes_ProblemTypeId",
                        column: x => x.ProblemTypeId,
                        principalTable: "ProblemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FixRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProblemId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ToId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixRequests_AspNetUsers_FromId",
                        column: x => x.FromId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixRequests_AspNetUsers_ToId",
                        column: x => x.ToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixRequests_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferFixRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProblemId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ToId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferFixRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferFixRequests_AspNetUsers_FromId",
                        column: x => x.FromId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferFixRequests_AspNetUsers_ToId",
                        column: x => x.ToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferFixRequests_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarBrandId",
                table: "Cars",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_FixRequests_FromId",
                table: "FixRequests",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_FixRequests_ProblemId",
                table: "FixRequests",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_FixRequests_ToId",
                table: "FixRequests",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferFixRequests_FromId",
                table: "OfferFixRequests",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferFixRequests_ProblemId",
                table: "OfferFixRequests",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferFixRequests_ToId",
                table: "OfferFixRequests",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_ProblemTypeId",
                table: "Problems",
                column: "ProblemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCars_AspNetUsers_PersonDataModelId",
                table: "OwnedCars",
                column: "PersonDataModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedCars_AspNetUsers_PersonDataModelId",
                table: "OwnedCars");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "FixRequests");

            migrationBuilder.DropTable(
                name: "OfferFixRequests");

            migrationBuilder.DropTable(
                name: "Problems");

            migrationBuilder.DropTable(
                name: "ProblemTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnedCars",
                table: "OwnedCars");

            migrationBuilder.RenameTable(
                name: "OwnedCars",
                newName: "OwnedCarDataModel");

            migrationBuilder.RenameIndex(
                name: "IX_OwnedCars_PersonDataModelId",
                table: "OwnedCarDataModel",
                newName: "IX_OwnedCarDataModel_PersonDataModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnedCarDataModel",
                table: "OwnedCarDataModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedCarDataModel_AspNetUsers_PersonDataModelId",
                table: "OwnedCarDataModel",
                column: "PersonDataModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
