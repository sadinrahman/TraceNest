using Microsoft.EntityFrameworkCore;
using TraceNest.Data;
using TraceNest.Models;

namespace TraceNest.Repository.ProfileRepositories
{
	public class ProfileRepository : IProfileRepository
	{
		private readonly AppDbContext _context;
		public ProfileRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<bool> UpdateUserAsync(User updateuser)
		{
			_context.users.Update(updateuser);

			return await _context.SaveChangesAsync() > 0;
		}
		public async Task<User> GetUserByIdAsync(Guid id)
		{
			return await _context.users.FirstOrDefaultAsync(x=>x.Id==id);
		}
	}
}
