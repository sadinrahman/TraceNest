using TraceNest.Models;

namespace TraceNest.Repository.AuthRepositories
{
	public interface IAuthRepository
	{
		Task<bool> AddUserAsync(User user);
		User GetUserByEmail(string email);
	}
}
