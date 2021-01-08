using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAdminPanel.Migrations
{
    public partial class AddNewStatsFieldFotBotSignal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllJobCount",
                table: "BotSignals",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "BotSignals",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllJobCount",
                table: "BotSignals");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "BotSignals");
        }
    }
}
