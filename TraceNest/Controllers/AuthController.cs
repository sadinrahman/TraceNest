using Microsoft.AspNetCore.Mvc;
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
				ModelState.AddModelError(string.Empty, result.Message);
				return View(loginDto);
			}

			return RedirectToAction("Index", "Home");
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
	}
}
