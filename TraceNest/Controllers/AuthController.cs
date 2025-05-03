using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TraceNest.Dto;
using TraceNest.Services.AuthServices;

namespace TraceNest.Controllers
{
	[ApiController]
	[Route("Auth")]
	public class AuthController : Controller
	{
		private readonly IAuthService _service;

		public AuthController(IAuthService service)
		{
			_service = service;
		}

		[HttpGet("Login")]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
		{
			var result = await _service.Login(loginDto);

			if (!result.Success)
			{ 
				return BadRequest(new { message = result.Message });
			}

			var token = result.Data;

			// Set the JWT as an HttpOnly cookie
			Response.Cookies.Append("AuthToken", token, new CookieOptions
			{
				HttpOnly = true,
				Secure = true, // Use true in production (HTTPS)
				SameSite = SameSiteMode.Strict,
				Expires = DateTimeOffset.UtcNow.AddHours(5) // Optional: set expiry
			});

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);
			var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
			if (role == null)
			{
				Console.WriteLine("Hlooooooooooooooooooooo");
			}
			Console.WriteLine(role);
			if (role == "admin")
			{
				return RedirectToAction("Index", "Admin");
			}
			
			return RedirectToAction("Home", "Home");
		}


		[HttpGet("Register")]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromForm] RegisterDto register)
		{
			if (!ModelState.IsValid)
			{
				return View(register);
			}

			var result = await _service.Register(register);

			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.Message ?? "Registration failed");
				return View(register);
			}

			// Registration success
			return RedirectToAction("Login", "Auth");
		}
		public IActionResult LogOut()
		{
			Response.Cookies.Delete("AuthToken");
			Response.Cookies.Delete("refreshToken");
			return RedirectToAction("Login", "Auth");
		}
	}
}
