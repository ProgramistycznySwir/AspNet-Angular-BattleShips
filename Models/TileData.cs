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

    // If amount of flags bloats up past 4 implement FlagEnum.
    public bool IsMiss { get; set; }
    public bool IsHit { get; set; }
    public int Player_SubID { get; set; }
}