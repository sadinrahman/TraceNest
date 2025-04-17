using TraceNest.Dto;
using TraceNest.Models;
using TraceNest.Repository.ProfileRepositories;

namespace TraceNest.Services.ProfileServices
{
	public class ProfileService: IProfileService
	{
		private readonly IProfileRepository _Repository;
		public ProfileService(IProfileRepository profileRepository)
		{
			_Repository = profileRepository;
		}
		public async Task<bool> UpdateUserAsync(UpdateUser updateuser,Guid id)
		{
			var user= await _Repository.GetUserByIdAsync(id);

			user.Username = updateuser.Username ?? user.Username;
			user.Email = updateuser.Email ?? user.Email;
				
			
			return await _Repository.UpdateUserAsync(user);
		}
		public async Task<UpdateUser> GetUserByIdAsync(Guid id)
		{
			var res= await _Repository.GetUserByIdAsync(id);
			if (res != null)
			{
				var user = new UpdateUser
				{
					Username = res.Username,
					Email = res.Email
				};
				return user;
			}
			else
			{
				return null;
			}
		}
	}
}
