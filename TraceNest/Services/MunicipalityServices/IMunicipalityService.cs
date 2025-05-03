using TraceNest.Dto;
using TraceNest.Models;

namespace TraceNest.Services.MunicipalityServices
{
	public interface IMunicipalityService
	{
		Task<List<Municipality>> GetAll();
		Task<Guid> AddMuncipality(string MuncipalityName);
		Task<bool> RemoveMuncipality(Guid id);
		Task<bool> UpdateMunicipality(MuncipalityDto municipality);
		Task<int> muncipalitycount();
		Task<string> GetMunicipalityName(Guid id);
	}
}
