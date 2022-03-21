using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM.API.Migrations.CurrentStateDatabase
{
    public partial class UpdateMessageContentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "MessageContents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MessageContents",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
