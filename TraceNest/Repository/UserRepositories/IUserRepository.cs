using TraceNest.Models;

namespace TraceNest.Repository.UserRepositories
{
	public interface IUserRepository
	{
		Task<List<User>> GetAllUsersAsync();
	}
}
