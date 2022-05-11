using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using StronglyTypedIds;

namespace HappyTeam_BattleShips.Models;


[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid,
        converters: StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct PlayerID{}

[Index(nameof(PublicID))]
public class Player
{
    [Key]
    public PlayerID ID { get; set; }
    /// <summary>
    /// Used when creating games, as ID is only visible to player, not to others.
    /// </summary>
    public PlayerID PublicID { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime LastUsedTime { get; set; }
    
    public ICollection<GamePlayer> Games { get; set; }
}