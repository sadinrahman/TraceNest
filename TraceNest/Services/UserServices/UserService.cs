using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TraceNest.Dto;
using TraceNest.Repository.ProfileRepositories;
using TraceNest.Repository.UserRepositories;

namespace TraceNest.Services.UserServices
{
	public class UserService: IUserService
	{
		private readonly IUserRepository _repository;
		private readonly IProfileRepository _profilrepo;
		public UserService(IUserRepository userRepository,IProfileRepository profile)
		{
			_repository = userRepository;
			_profilrepo = profile;
		}
		public async Task<List<UserViewDto>> GetAll()
		{
			var res = await _repository.GetAllUsersAsync();
			if (res != null)
			{
				var userDtos = new List<UserViewDto>();
				foreach (var user in res)
				{
					userDtos.Add(new UserViewDto
					{
						Id = user.Id,
						Username = user.Username,
						Email = user.Email,
						IsActive = user.IsActive
					});
				}
				return userDtos;
			}
			else
			{
				return null;
			}
		}
		public async Task<bool> BlockUnblock(Guid userid)
		{
			var user =await _profilrepo.GetUserByIdAsync(userid);
			if (user != null)
			{
				user.IsActive = !user.IsActive;
				return await _profilrepo.UpdateUserAsync(user);
			}
			else
			{
				return false;
			}
		}
		public async Task<int> Countusers()
		{
			var result = await _repository.GetAllUsersAsync();
			return result != null ? result.Count : 0;
		}
	}
}
