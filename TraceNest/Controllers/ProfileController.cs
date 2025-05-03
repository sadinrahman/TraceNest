using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;
using TraceNest.Dto;
using TraceNest.Models;
using TraceNest.Services.CategoryServices;
using TraceNest.Services.FoundServices;
using TraceNest.Services.LostServices;
using TraceNest.Services.MunicipalityServices;
using TraceNest.Services.ProfileServices;

namespace TraceNest.Controllers
{
    [Route("Profile")]
	[Authorize]
	public class ProfileController : Controller
    {
        private readonly IProfileService _service;
		private readonly IFoundService _foundService;
		private readonly ILostService _lostService;
		public readonly ICategoryService _categoryService;
		public readonly IMunicipalityService _ser;
		public ProfileController(IProfileService service,ILostService lostService,IFoundService foundService,ICategoryService category,IMunicipalityService municipality)
		{
			_service = service;
			_foundService = foundService;
			_lostService = lostService;
			_categoryService = category;
			_ser = municipality;
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
		public async Task<IActionResult> FoundPost()
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res =await _foundService.GetByUser(userid);
				return View(res);
			}
			return RedirectToAction("Login", "Auth");
			
		}
		[HttpGet("LostPosts")]
		public async Task<IActionResult> LostPosts()
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res =await _lostService.GetPostBySpecificUser(userid);
				return View(res);
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpGet("EditFound")]
		public async Task<IActionResult> EditFound(Guid id)
		{
			var hlo =await _ser.GetAll();
			var category =await _categoryService.GetAll();
			ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res =await _foundService.GetByUser(userid);
				var result = res.FirstOrDefault(x => x.Id == id);
				return View(result);
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpPost("EditFound")]
		public async Task<IActionResult> EditFound(UpdateFoundDto update)
		{

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res=await _foundService.UpdateFound(update, userid);
				return RedirectToAction("FoundPost");
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpGet("EditLost")]
		public async Task<IActionResult> EditLost(Guid id)
		{
			var hlo =await _ser.GetAll();
			var category =await _categoryService.GetAll();
			ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var ress =await _lostService.GetPostBySpecificUser(userid);
				var res = ress.FirstOrDefault(x => x.Id == id);
				return View(res);
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpPost("EditLost")]
		public async Task<IActionResult> EditLost(UpdateLostDto update)
		{

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res =await _lostService.UpdateLost(update, userid);
				return RedirectToAction("LostPosts");
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpGet("DeleteLost")]
		public async Task<IActionResult> DeleteLost(Guid id)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res =await _lostService.DeleteLost(id, userid);
				return RedirectToAction("LostPosts", "Profile");
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpGet("DeleteFound")]
		public async Task<IActionResult> DeleteFound(Guid id)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res =await _foundService.DeleteLost(id, userid);
				return RedirectToAction("FoundPost", "Profile");
			}
			return RedirectToAction("Login", "Auth");
		}
	}
}
