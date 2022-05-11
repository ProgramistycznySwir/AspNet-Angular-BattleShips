using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HappyTeam_BattleShips.Models;


[Index(nameof(PublicID))]
public class Player
{
    [Key]
    public Guid ID { get; set; }
    /// <summary>
    /// Used when creating games, as ID is only visible to player, not to others.
    /// </summary>
    public Guid PublicID { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime LastUsedTime { get; set; }
    
    public ICollection<GamePlayer> Games { get; set; }
}

public static class Player_Ext
{
    public static Player GetSanitised(this Player self)
        => new Player {
                ID = Guid.Empty,
                PublicID = self.PublicID,
                CreationTime = self.CreationTime,
                LastUsedTime = self.LastUsedTime,
            };
}