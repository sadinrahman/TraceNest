using TraceNest.Models;

namespace TraceNest.Repository.MunicipalityRepositories
{
	public interface IMunicipalityRepository
	{
		Task<bool> AddAsync(Municipality municipality);
		List<Municipality> GetAllAsync();
	}
}
