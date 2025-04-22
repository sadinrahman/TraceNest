using TraceNest.Models;

namespace TraceNest.Repository.MunicipalityRepositories
{
	public interface IMunicipalityRepository
	{
		Task<bool> AddAsync(Municipality municipality);
		Task<List<Municipality>> GetAllAsync();
		Task<Guid> GetCategoryID(string category);
		Task<bool> RemoveMuncipality(Municipality municipality);
		Task<bool> UpdateMunicipality(Municipality category);
		Task<Municipality> GetMunicipalityById(Guid id);
	}
}
