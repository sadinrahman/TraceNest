using TraceNest.Dto;

namespace TraceNest.Services.LostServices
{
	public interface ILostService
	{
		public bool PostLostProduct(LostAddDto dto, Guid userid, IFormFile Image);
		List<LostItemDto> GetAll();
	}
}
