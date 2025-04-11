using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using TraceNest.Data;
using TraceNest.Models;

namespace TraceNest.Repository.LostRepositories
{
	public class LostRepository: ILostRepository
	{
		private readonly AppDbContext _context;
		public LostRepository(AppDbContext context)
		{
			_context = context;
		}
		public bool AddAsync(Lost lost)
		{
			_context.Add(lost);
			Console.WriteLine("hiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii");
			_context.SaveChanges();
			Console.WriteLine("hloooooooooooooooooooooooooooooooooooooooooooooooooooo");
			return true;
		}
		public List<Lost> GetAll()
		{
			var losts = _context.Losts.Include(c => c.Municipality).Include(y=>y.Category).ToList();
			return losts;
		}
	}
}
