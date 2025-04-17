using TraceNest.Models;

namespace TraceNest.Repository.ProfileRepositories
{
	public interface IProfileRepository
	{
		Task<bool> UpdateUserAsync(User updateuser);
		Task<User> GetUserByIdAsync(Guid id);
	}
}
