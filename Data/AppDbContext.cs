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

			// Model id's mappings:
		//     foreach (var entityType in builder.Model.GetEntityTypes())
		//         foreach(var prop in entityType.ClrType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
		//             switch(prop.PropertyType.Name)
		//             {
		//                 case nameof(AchievementID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new AchievementID.EfCoreValueConverter()); break;
		//                 case nameof(AchievementCategoryID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new AchievementCategoryID.EfCoreValueConverter()); break;
		//                 case nameof(AreaID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new AreaID.EfCoreValueConverter()); break;
		//                 case nameof(ChatID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new ChatID.EfCoreValueConverter()); break;
		//                 case nameof(ChatParticipantSubID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new ChatParticipantSubID.EfCoreValueConverter()); break;
		//                 case nameof(CommitteeID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new CommitteeID.EfCoreValueConverter()); break;
		//                 case nameof(EmployeeID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new EmployeeID.EfCoreValueConverter()); break;
		//                 case nameof(EmployeePositionID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new EmployeePositionID.EfCoreValueConverter()); break;
		//                 case nameof(ProposalID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new ProposalID.EfCoreValueConverter()); break;
		//                 case nameof(RatingID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new RatingID.EfCoreValueConverter()); break;
		//                 case nameof(SummaryRatingID): builder.Entity(entityType.Name).Property(prop.Name).HasConversion(new SummaryRatingID.EfCoreValueConverter()); break;
		//                 default: continue;
		//             }
			

			builder.Entity<GamePlayer>()
					// .Ignore(e => e.Game)
					// .Ignore(e => e.Player)
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
			
			// builder.Entity<AchievementCategory>() // AchievementCategory -< Achievement
			// 		.HasMany<Achievement>(e => e.Achievements)
			// 		.WithOne(e => e.Category)
			// 		.HasForeignKey(e => e.Category_ID);
		}
	}
}