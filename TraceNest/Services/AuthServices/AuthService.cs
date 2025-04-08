using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TraceNest.Dto;
using TraceNest.Models;
using TraceNest.Repository.AuthRepositories;

namespace TraceNest.Services.AuthServices
{
	public class AuthService : IAuthService
	{
		private readonly IAuthRepository _repository;
		private readonly IConfiguration _config;

		public AuthService(IAuthRepository repository, IConfiguration config)
		{
			_repository = repository;
			_config = config;
		}

		public async Task<ServiceResponse<string>> Login(LoginDto login)
		{
			var user = _repository.GetUserByEmail(login.Email);

			if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
			{
				return new ServiceResponse<string>
				{
					Success = false,
					Message = "Invalid username or password"
				};
			}

			if (!user.IsActive)
			{
				return new ServiceResponse<string>
				{
					Success = false,
					Message = "You are blocked"
				};
			}

			string token = GenerateToken(user);

			return new ServiceResponse<string>
			{
				Success = true,
				Data = token
			};
		}

		public async Task<ServiceResponse<bool>> Register(RegisterDto register)
		{
			try
			{
				var existingUser = _repository.GetUserByEmail(register.Email);
				if (existingUser != null)
				{
					return new ServiceResponse<bool>
					{
						Success = false,
						Message = "Email is already registered",
						Data = false
					};
				}

				register.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);
				var user = new User
				{
					Username = register.UserName,
					Email = register.Email,
					Password = register.Password
				};

				await _repository.AddUserAsync(user);

				return new ServiceResponse<bool>
				{
					Success = true,
					Message = "Registration successful",
					Data = true
				};
			}
			catch (Exception ex)
			{
				return new ServiceResponse<bool>
				{
					Success = false,
					Message = $"An error occurred: {ex.Message}",
					Data = false
				};
			}
		}

		private string GenerateToken(User user)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.Role, user.Role),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
