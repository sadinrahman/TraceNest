using Microsoft.EntityFrameworkCore;
using TraceNest.Data;
using TraceNest.Models;

namespace TraceNest.Repository.UserRepositories
{
	public class UserRepository: IUserRepository
	{
		private readonly AppDbContext _context;
		public UserRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<User>> GetAllUsersAsync()
		{
			return await _context.users.Where(x=>x.Role!="Admin").ToListAsync();
		}
	}
}
