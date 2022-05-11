using System.ComponentModel.DataAnnotations;

namespace HappyTeam_BattleShips.Models;


public class GamePlayer
{
    [Key]
    public int subID { get; set; }
    [Key]
    public Guid Game_ID { get; set; }
    public Game Game { get; set; }

    public Guid Player_ID { get; set; }
    public Player Player { get; set; }
}