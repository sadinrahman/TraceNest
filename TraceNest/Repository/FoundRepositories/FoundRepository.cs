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
		public async Task<bool> Add(Found found)
		{
			_context.Found.Add(found);
			await _context.SaveChangesAsync();
			return true;
			
		}
		public async Task<List<Found>> GetAll()
		{
			var found =await _context.Found.Include(c => c.Municipality).Include(y => y.Category).Where(x=>x.Status=="Pending").ToListAsync();
			return found;
		}
		public async Task<List<Found>> GetPostBySpecificUser(Guid userid)
		{
			var found =await _context.Found.Include(c => c.Municipality).Include(y => y.Category).Where(x => x.UserId == userid).ToListAsync();
			return found;
		}
		public async Task<bool> Update(Found found)
		{
			_context.Found.Update(found);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> Delete(Found found)
		{
			_context.Found.Remove(found);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<List<Found>> GetAllFounded()
		{
			var found = await _context.Found.Include(c => c.Municipality).Include(y => y.Category).ToListAsync();
			return found;
		}
	}
}
