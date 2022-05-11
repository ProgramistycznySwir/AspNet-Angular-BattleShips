using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyTeam_BattleShips.Migrations
{
    public partial class ChangedTileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "subID",
                table: "GamePlayer",
                newName: "SubID");

            migrationBuilder.AddColumn<bool>(
                name: "IsHit",
                table: "TileData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMiss",
                table: "TileData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Player_SubID",
                table: "TileData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHit",
                table: "TileData");

            migrationBuilder.DropColumn(
                name: "IsMiss",
                table: "TileData");

            migrationBuilder.DropColumn(
                name: "Player_SubID",
                table: "TileData");

            migrationBuilder.RenameColumn(
                name: "SubID",
                table: "GamePlayer",
                newName: "subID");
        }
    }
}
