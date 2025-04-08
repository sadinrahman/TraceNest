using TraceNest.Models;

namespace TraceNest.Repository.LostRepositories
{
	public interface ILostRepository
	{
		bool Add(Lost lost);
	}
}
