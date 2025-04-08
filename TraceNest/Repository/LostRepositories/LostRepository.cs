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
		public bool Add(Lost lost)
		{
			_context.Add(lost);
			_context.SaveChanges();
			return true;
		}
	}
}
