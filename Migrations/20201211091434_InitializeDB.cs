using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebAdminPanel.Migrations
{
    public partial class InitializeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBase_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BotSignals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobLinks = table.Column<string>(type: "text", nullable: true),
                    CoverrLetter = table.Column<string>(type: "text", nullable: true),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    BotTypeSignal = table.Column<int>(type: "integer", nullable: false),
                    IgnoreAlreadySended = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotSignals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotSignals_AccountBase_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AccountBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BotSignals_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: true),
                    CoverLetter = table.Column<string>(type: "text", nullable: true),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobBase_AccountBase_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AccountBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBase_SiteId",
                table: "AccountBase",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BotSignals_AccountId",
                table: "BotSignals",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BotSignals_SiteId",
                table: "BotSignals",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBase_AccountId",
                table: "JobBase",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotSignals");

            migrationBuilder.DropTable(
                name: "JobBase");

            migrationBuilder.DropTable(
                name: "AccountBase");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
