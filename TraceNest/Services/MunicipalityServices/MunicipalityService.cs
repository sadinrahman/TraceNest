using TraceNest.Models;
using TraceNest.Repository.MunicipalityRepositories;

namespace TraceNest.Services.MunicipalityServices
{
	public class MunicipalityService: IMunicipalityService
	{
		private readonly IMunicipalityRepository _repo;
		public MunicipalityService(IMunicipalityRepository repo)
		{
			_repo = repo;
		}
		public async Task<bool> AddMunicipality(string MuncipalityName)
		{
			var res = new Models.Municipality
			{
				MunicipalityName = MuncipalityName
			};
			var result = await _repo.AddAsync(res);
			if (!result)
			{
				return false;
			}
			return true;
		}
		public  List<Municipality> GetAll()
		{
			return _repo.GetAllAsync();
		}
	}
}
