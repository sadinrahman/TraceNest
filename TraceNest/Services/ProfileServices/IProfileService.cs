using TraceNest.Dto;
using TraceNest.Models;

namespace TraceNest.Services.ProfileServices
{
	public interface IProfileService
	{
		Task<UpdateUser> GetUserByIdAsync(Guid id);
		Task<bool> UpdateUserAsync(UpdateUser updateuser, Guid id);
	}
}
