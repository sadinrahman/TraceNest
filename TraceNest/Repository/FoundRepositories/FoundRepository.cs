using Microsoft.EntityFrameworkCore;
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
		public List<Found> GetAll()
		{
			var found = _context.Found.Include(c => c.Municipality).Include(y => y.Category).Where(x=>x.Status=="Pending").ToList();
			return found;
		}
		public List<Found> GetPostBySpecificUser(Guid userid)
		{
			var found = _context.Found.Include(c => c.Municipality).Include(y => y.Category).Where(x => x.UserId == userid).ToList();
			return found;
		}
		public bool Update(Found found)
		{
			_context.Found.Update(found);
			_context.SaveChanges();
			return true;
		}
		public bool Delete(Found found)
		{
			_context.Found.Remove(found);
			_context.SaveChanges();
			return true;
		}
	}
}
