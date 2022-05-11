using System.ComponentModel.DataAnnotations;
using StronglyTypedIds;

namespace HappyTeam_BattleShips.Models;


[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid,
        converters: StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct PlayerID{}

public class Player
{
    [Key]
    public PlayerID ID { get; set; }
    public ICollection<Game> Games { get; set; }
}