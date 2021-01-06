using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAdminPanel.Migrations
{
    public partial class AddMaxPageCountField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPageCount",
                table: "BotSignals",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPageCount",
                table: "BotSignals");
        }
    }
}
