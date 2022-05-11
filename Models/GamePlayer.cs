using System.ComponentModel.DataAnnotations;
using StronglyTypedIds;

namespace HappyTeam_BattleShips.Models;


[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int,
        converters: StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct GamePlayerSubID{}

public class GamePlayer
{
    [Key]
    public GamePlayerSubID subID { get; set; }
    [Key]
    public GameID Game_ID { get; set; }
    public Game Game { get; set; }

    public PlayerID Player_ID { get; set; }
    public Player Player { get; set; }
}