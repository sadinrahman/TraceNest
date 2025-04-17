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
		
		public  List<Municipality> GetAll()
		{
			return _repo.GetAllAsync();
		}
		public async Task<Guid> AddMuncipality(string MuncipalityName)
		{
			var res = new Municipality
			{
				MunicipalityName = MuncipalityName
			};
			var result = _repo.AddAsync(res);
			var Id=await _repo.GetCategoryID(MuncipalityName);
			return Id;

		}
	}
}
