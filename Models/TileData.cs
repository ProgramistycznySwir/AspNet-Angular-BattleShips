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

    public bool ShipSize { get; set; }
    public static readonly List<byte> ShipSizes = new List<byte> { 2, 3, 3, 4, 5 };
}

public static class TileData_Ext
{
    public static TileData GetSanitised(this TileData self)
    {
        return new TileData {
            Game_ID= self.Game_ID,
            Game= null,
            X= self.X,
            Y= self.Y,
            IsMiss= self.IsMiss,
            IsHit= self.IsHit,
            Player_SubID= self.Player_SubID,
            ShipSize= self.ShipSize
        };
    }
}