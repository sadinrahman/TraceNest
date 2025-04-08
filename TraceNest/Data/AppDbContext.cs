using Microsoft.EntityFrameworkCore;
using TraceNest.Models;

namespace TraceNest.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<User> users { get; set; }
		public DbSet<Lost> Losts { get; set; }
	}
}
