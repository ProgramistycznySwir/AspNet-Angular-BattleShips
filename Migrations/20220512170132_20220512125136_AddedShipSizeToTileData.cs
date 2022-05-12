using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyTeam_BattleShips.Migrations
{
    public partial class _20220512125136_AddedShipSizeToTileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "ShipSize",
                table: "TileData",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipSize",
                table: "TileData");
        }
    }
}
