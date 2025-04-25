using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using System.Threading.Tasks;
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
		public async Task<bool> AddAsync(Lost lost)
		{
			_context.Add(lost);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<List<Lost>> GetAll()
		{
			var losts =await _context.Losts.Include(c => c.Municipality).Include(y=>y.Category).Where(x => x.Status == "Pending").ToListAsync();
			return losts;
		}
		public async Task<List<Lost>> GetPostBySpecificUser(Guid userid)
		{
			var losts =await _context.Losts.Include(c => c.Municipality).Include(y => y.Category).Where(x=>x.UserId==userid).ToListAsync();
			return losts;
		}
		public async  Task<bool> Update(Lost lost)
		{
			_context.Losts.Update(lost);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> Delete(Lost lost)
		{
			_context.Losts.Remove(lost);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<List<Lost>> GetAllLosted()
		{
			var losts = await _context.Losts.Include(c => c.Municipality).Include(y => y.Category).ToListAsync();
			return losts;
		}
	}
}
