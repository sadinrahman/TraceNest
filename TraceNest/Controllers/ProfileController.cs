using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TraceNest.Dto;
using TraceNest.Services.ProfileServices;

namespace TraceNest.Controllers
{
    [Route("Profile")]
	public class ProfileController : Controller
    {
        private readonly IProfileService _service;
		public ProfileController(IProfileService service)
		{
			_service = service;
		}
		[HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userId))
			{
				var res =await _service.GetUserByIdAsync( userId);
				return View(res);
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpPost("Index")]
		public async Task<IActionResult> Index([FromForm] UpdateUser updateuser)
		{
			if (!ModelState.IsValid)
			{
				foreach (var state in ModelState)
				{
					foreach (var error in state.Value.Errors)
					{
						Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");
					}
				}
				return View(updateuser);
			}
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userId))
			{
				var res = await _service.UpdateUserAsync(updateuser, userId);
				if (res)
				{
					var user = await _service.GetUserByIdAsync(userId);
					return View(user);
				}
			}
			return RedirectToAction("Login", "Auth");
		}
	
		[HttpGet("FoundPost")]
		public IActionResult FoundPost()
		{
			return View();
		}
		[HttpGet("LostPosts")]
		public IActionResult LostPosts()
		{
			return View();
		}
	}
}
