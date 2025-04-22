using TraceNest.Dto;

namespace TraceNest.Services.FoundServices
{
	public interface IFoundService
	{
		Task<bool> PostFoundProduct(FoundPostDto dto, Guid userid, IFormFile Image);
		Task<List<FoundItemDto>> GetAll();
		Task<List<FoundItemDto>> GetByUser(Guid userid);
		Task<bool> UpdateFound(UpdateFoundDto found, Guid userid);
		Task<bool> DeleteLost(Guid id, Guid userid);
	}
}
