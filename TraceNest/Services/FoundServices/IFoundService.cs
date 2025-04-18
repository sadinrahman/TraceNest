using TraceNest.Dto;

namespace TraceNest.Services.FoundServices
{
	public interface IFoundService
	{
		bool PostFoundProduct(FoundPostDto dto, Guid userid, IFormFile Image);
		List<FoundItemDto> GetAll();
		List<FoundItemDto> GetByUser(Guid userid);
		bool UpdateFound(UpdateFoundDto found, Guid userid);
		bool DeleteLost(Guid id, Guid userid);
	}
}
