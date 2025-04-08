using TraceNest.Dto;
using TraceNest.Repository.LostRepositories;

namespace TraceNest.Services.LostServices
{
	public class LostService: ILostService
	{
		private readonly ILostRepository _repo;
		public LostService(ILostRepository repo)
		{
			_repo = repo;
		}
		public async Task<bool> PostLostProduct(LostAddDto dto,Guid userid)
		{
			
		}
	}
}
