using TraceNest.Data;
using TraceNest.Models;

namespace TraceNest.Repository.FoundRepositories
{
	public class FoundRepository: IFoundRepository
	{
		private readonly AppDbContext _context;
		public FoundRepository(AppDbContext context)
		{
			_context = context;
		}
		public bool Add(Found found)
		{
			_context.Found.Add(found);
			_context.SaveChanges();
			return true;
			
		}

	}
}
