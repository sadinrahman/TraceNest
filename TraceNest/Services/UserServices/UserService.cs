using System.Runtime.CompilerServices;
using TraceNest.Dto;
using TraceNest.Repository.UserRepositories;

namespace TraceNest.Services.UserServices
{
	public class UserService: IUserService
	{
		private readonly IUserRepository _repository;
		public UserService(IUserRepository userRepository)
		{
			_repository = userRepository;
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
	}
}
