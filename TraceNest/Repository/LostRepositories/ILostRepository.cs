using TraceNest.Models;

namespace TraceNest.Repository.LostRepositories
{
	public interface ILostRepository
	{
		Task<bool> AddAsync(Lost lost);
		Task<List<Lost>> GetAll();
		Task<List<Lost>> GetPostBySpecificUser(Guid userid);
		Task<bool> Update(Lost lost);
		Task<bool> Delete(Lost lost);
		Task<List<Lost>> GetAllLosted();
	}
}
