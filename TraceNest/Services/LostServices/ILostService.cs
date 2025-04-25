using TraceNest.Dto;

namespace TraceNest.Services.LostServices
{
	public interface ILostService
	{
		Task<bool> PostLostProduct(LostAddDto dto, Guid userid, IFormFile Image);
		Task<List<LostItemDto>> GetAll();
		Task<List<LostItemDto>> GetPostBySpecificUser(Guid userid);
		Task<bool> UpdateLost(UpdateLostDto found, Guid userid);
		Task<bool> DeleteLost(Guid id, Guid userid);
		Task<int> LostCount();
	}
}
