using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAdminPanel.Migrations
{
    public partial class AddFlexJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobPowerToFly_SignalId",
                table: "Jobs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BotSignalPowerToFly_AccountId",
                table: "BotSignals",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "BotSignals",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountPowerToFly_SiteId",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobPowerToFly_SignalId",
                table: "Jobs",
                column: "JobPowerToFly_SignalId");

            migrationBuilder.CreateIndex(
                name: "IX_BotSignals_BotSignalPowerToFly_AccountId",
                table: "BotSignals",
                column: "BotSignalPowerToFly_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountPowerToFly_SiteId",
                table: "Accounts",
                column: "AccountPowerToFly_SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Sites_AccountPowerToFly_SiteId",
                table: "Accounts",
                column: "AccountPowerToFly_SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BotSignals_Accounts_BotSignalPowerToFly_AccountId",
                table: "BotSignals",
                column: "BotSignalPowerToFly_AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_BotSignals_JobPowerToFly_SignalId",
                table: "Jobs",
                column: "JobPowerToFly_SignalId",
                principalTable: "BotSignals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Sites_AccountPowerToFly_SiteId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BotSignals_Accounts_BotSignalPowerToFly_AccountId",
                table: "BotSignals");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_BotSignals_JobPowerToFly_SignalId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobPowerToFly_SignalId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_BotSignals_BotSignalPowerToFly_AccountId",
                table: "BotSignals");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountPowerToFly_SiteId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "JobPowerToFly_SignalId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "BotSignalPowerToFly_AccountId",
                table: "BotSignals");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "BotSignals");

            migrationBuilder.DropColumn(
                name: "AccountPowerToFly_SiteId",
                table: "Accounts");
        }
    }
}
