using HappyTeam_BattleShips.Data;
using HappyTeam_BattleShips.Models;
using HappyTeam_BattleShips.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HappyTeam_BattleShips.Services;

public class PlayerService : IPlayerService
{
	private readonly AppDbContext _context;

    public PlayerService(AppDbContext context)
    {
        _context = context;
    }

    public Player AddPlayer()
    {
        var player = new Player {
            ID = Guid.NewGuid(),
            PublicID = Guid.NewGuid(),
            CreationTime = DateTime.Now,
            LastUsedTime = DateTime.Now,
        };
        _context.Add(player);
        _context.SaveChanges();
        return player;
    }

    public Player GetPlayer(Guid id)
        => _context.Players.Where(e => e.ID == id)
                .Include(e => e.Games)
                .FirstOrDefault()!;

    public Player GetPlayerByPublicID(Guid publicID)
        => _context.Players
                .FirstOrDefault(player => player.PublicID.Equals(publicID))
                ?.GetSanitised()!;
}