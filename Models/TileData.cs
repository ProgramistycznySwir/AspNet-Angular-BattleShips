using System.ComponentModel.DataAnnotations;

namespace HappyTeam_BattleShips.Models;

public class TileData
{
    [Key]
    public Guid Game_ID { get; set; }
    public Game Game { get; set; }
    [Key]
    public byte X { get; set; }
    [Key]
    public byte Y { get; set; }
    public enum TileState { Ship_P1, Ship_P2, ShipHit_P1, ShipHit_P2, Miss_P1, Miss_P2 }
    public TileState State;
}