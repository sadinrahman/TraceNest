using TraceNest.Models;

namespace TraceNest.Services.MunicipalityServices
{
	public interface IMunicipalityService
	{
		List<Municipality> GetAll();
		Task<Guid> AddMuncipality(string MuncipalityName);
	}
}
