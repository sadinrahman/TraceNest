using Microsoft.EntityFrameworkCore;
using TraceNest.Models;

namespace TraceNest.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<User> users { get; set; }
		public DbSet<Lost> Losts { get; set; }
		public DbSet<Found> Found { get; set; }
		public DbSet<Municipality> Municipality { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Messages> Messages { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasKey(u => u.Id);

			modelBuilder.Entity<User>()
				.HasMany(u => u.Losts)
				.WithOne(l => l.User)
				.HasForeignKey(l => l.UserId);

			modelBuilder.Entity<Lost>()
				.HasOne(x=>x.Municipality)
				.WithMany(x => x.Lost)
				.HasForeignKey(x => x.MunicipalityId);

			modelBuilder.Entity<Lost>()
				.HasOne(x => x.Category)
				.WithMany(x => x.Losts)
				.HasForeignKey(x => x.CategoryId);


			modelBuilder.Entity<User>()
				.HasMany(u => u.Founds)
				.WithOne(l => l.User)
				.HasForeignKey(l => l.UserId);

			modelBuilder.Entity<Found>()
				.HasOne(x => x.Municipality)
				.WithMany(x => x.Found)
				.HasForeignKey(x => x.MunicipalityId);

			modelBuilder.Entity<Found>()
				.HasOne(x => x.Category)
				.WithMany(x => x.Found)
				.HasForeignKey(x => x.CategoryId);

			modelBuilder.Entity<Messages>()
				.HasOne(m => m.Sender)
				.WithMany(u => u.SendMessages)
				.HasForeignKey(m => m.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Messages>()
				.HasOne(m => m.Receiver)
				.WithMany(u => u.RecievedMessages)
				.HasForeignKey(m => m.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
