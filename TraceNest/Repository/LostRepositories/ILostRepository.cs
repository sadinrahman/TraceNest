using TraceNest.Models;

namespace TraceNest.Repository.LostRepositories
{
	public interface ILostRepository
	{
		bool AddAsync(Lost lost);
		List<Lost> GetAll();
		List<Lost> GetPostBySpecificUser(Guid userid);
		bool Update(Lost lost);
		bool Delete(Lost lost);
	}
}
