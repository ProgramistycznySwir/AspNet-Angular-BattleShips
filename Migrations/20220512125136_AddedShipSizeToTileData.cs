using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyTeam_BattleShips.Migrations
{
    public partial class AddedShipSizeToTileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShipSize",
                table: "TileData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipSize",
                table: "TileData");
        }
    }
}
