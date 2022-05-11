using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyTeam_BattleShips.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastMove = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Turn = table.Column<int>(type: "INTEGER", nullable: false),
                    Result = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TileData",
                columns: table => new
                {
                    Game_ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    X = table.Column<byte>(type: "INTEGER", nullable: false),
                    Y = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TileData", x => new { x.Game_ID, x.X, x.Y });
                    table.ForeignKey(
                        name: "FK_TileData_Games_Game_ID",
                        column: x => x.Game_ID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlayer",
                columns: table => new
                {
                    subID = table.Column<int>(type: "INTEGER", nullable: false),
                    Game_ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Player_ID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayer", x => new { x.subID, x.Game_ID });
                    table.ForeignKey(
                        name: "FK_GamePlayer_Games_Game_ID",
                        column: x => x.Game_ID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayer_Players_Player_ID",
                        column: x => x.Player_ID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayer_Game_ID",
                table: "GamePlayer",
                column: "Game_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayer_Player_ID",
                table: "GamePlayer",
                column: "Player_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlayer");

            migrationBuilder.DropTable(
                name: "TileData");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
