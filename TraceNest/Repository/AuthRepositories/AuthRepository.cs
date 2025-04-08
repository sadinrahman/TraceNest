using TraceNest.Data;
using TraceNest.Models;

namespace TraceNest.Repository.AuthRepositories
{
	public class AuthRepository: IAuthRepository
	{
		private readonly AppDbContext _context;
		public AuthRepository(AppDbContext context)
		{
			_context = context;
		}
		public async  Task<bool> AddUserAsync(User user)
		{
			await _context.AddAsync(user);
			await _context.SaveChangesAsync();
			return true;
		}	
		public User GetUserByEmail(string email)
		{
			return _context.users.FirstOrDefault(x => x.Email == email);
		}
	}
}
