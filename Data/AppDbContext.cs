using System.Reflection;
using HappyTeam_BattleShips.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyTeam_BattleShips.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Game> Games { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<TileData> TileData { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<GamePlayer>()
					.HasKey(e => new { e.SubID, e.Game_ID });
			
			builder.Entity<Game>()
					.HasMany<GamePlayer>(e => e.Players)
					.WithOne(e => e.Game)
					.HasForeignKey(e => e.Game_ID);
			builder.Entity<Player>()
					.HasMany<GamePlayer>(e => e.Games)
					.WithOne(e => e.Player)
					.HasForeignKey(e => e.Player_ID);
			
			builder.Entity<Game>()
					.HasMany<TileData>(e => e.BoardData)
					.WithOne(e => e.Game)
					.HasForeignKey(e => e.Game_ID);
			
			builder.Entity<TileData>()
					.HasKey(e => new { e.Game_ID, e.X, e.Y });
		}
	}
}