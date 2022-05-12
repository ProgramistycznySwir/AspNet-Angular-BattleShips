using System.ComponentModel.DataAnnotations;

namespace HappyTeam_BattleShips.Models;


public class GamePlayer
{
    [Key]
    public int SubID { get; set; }
    [Key]
    public Guid Game_ID { get; set; }
    public Game Game { get; set; }


    public Guid Player_ID { get; set; }
    public Player Player { get; set; }
}

public static class GamePlayer_Ext
{
    /// <summary>
    /// When sanitising Player it replaces it's id with public id.
    /// </summary>
    public static GamePlayer GetSanitised(this GamePlayer self)
    {
        return new GamePlayer {
            SubID= self.SubID,
            Game_ID= self.Game_ID,
            Game= null,
            Player_ID= self.Player.PublicID,
            Player= null
        };
    }
}