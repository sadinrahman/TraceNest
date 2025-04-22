using TraceNest.Models;

namespace TraceNest.Repository.FoundRepositories
{
	public interface IFoundRepository
	{
		Task<bool> Add(Found found);
		Task<List<Found>> GetAll();
		Task<List<Found>> GetPostBySpecificUser(Guid userid);
		Task<bool> Update(Found found);
		Task<bool> Delete(Found found);
	}
}
