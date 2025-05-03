using System.Threading.Tasks;
using TraceNest.Dto;
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
		
		public  async Task<List<Municipality>> GetAll()
		{
			return await _repo.GetAllAsync();
		}
		public async Task<Guid> AddMuncipality(string MuncipalityName)
		{
			var res = new Municipality
			{
				MunicipalityName = MuncipalityName
			};
			var result = await _repo.AddAsync(res);
			var Id=await _repo.GetCategoryID(MuncipalityName);
			return Id;

		}
		public async Task<bool> RemoveMuncipality(Guid id)
		{
			var municipality =await _repo.GetMunicipalityById(id);
			if (municipality == null)
			{
				return false;
			}
			return await _repo.RemoveMuncipality(municipality);
		}
		public async Task<bool> UpdateMunicipality(MuncipalityDto municipality)
		{
			var res =await _repo.GetMunicipalityById(municipality.Id);
			if (res == null)
			{
				return false;
			}
			res.MunicipalityName = municipality.Name;
			return await _repo.UpdateMunicipality(res);

		}
		public async Task<int> muncipalitycount()
		{
			var res = await _repo.GetAllAsync();
			return res.Count();
		}
		public async Task<string> GetMunicipalityName(Guid id)
		{
			var res = await _repo.GetMunicipalityById(id);
			if (res == null)
			{
				return null;
			}
			return res.MunicipalityName;
		}
	}
}
