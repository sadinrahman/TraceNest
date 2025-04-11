using TraceNest.Models;

namespace TraceNest.Repository.LostRepositories
{
	public interface ILostRepository
	{
		bool AddAsync(Lost lost);
		List<Lost> GetAll();
	}
}
