using TraceNest.Dto;

namespace TraceNest.Services.FoundServices
{
	public interface IFoundService
	{
		bool PostFoundProduct(FoundPostDto dto, Guid userid, IFormFile Image);
		List<FoundItemDto> GetAll();
	}
}
