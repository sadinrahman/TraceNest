using System.Threading.Tasks;
using TraceNest.Dto;

namespace TraceNest.Services.AuthServices
{
	public interface IAuthService
	{
		Task<ServiceResponse<string>> Login(LoginDto login);
		Task<ServiceResponse<bool>> Register(RegisterDto register);
	}
}
