using TraceNest.Dto;

namespace TraceNest.Services.UserServices
{
	public interface IUserService
	{
		Task<List<UserViewDto>> GetAll();
	}
}
