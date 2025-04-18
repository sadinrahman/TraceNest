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
		public IActionResult FoundPost()
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res = _foundService.GetByUser(userid);
				return View(res);
			}
			return RedirectToAction("Login", "Auth");
			
		}
		[HttpGet("LostPosts")]
		public IActionResult LostPosts()
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res = _lostService.GetPostBySpecificUser(userid);
				return View(res);
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpGet("EditFound")]
		public IActionResult EditFound(Guid id)
		{
			var hlo = _ser.GetAll();
			var category = _categoryService.GetAll();
			ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res = _foundService.GetByUser(userid).FirstOrDefault(x=>x.Id==id);
				return View(res);
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpPost("EditFound")]
		public IActionResult EditFound(UpdateFoundDto update)
		{

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res=_foundService.UpdateFound(update, userid);
				return RedirectToAction("FoundPost");
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpGet("EditLost")]
		public IActionResult EditLost(Guid id)
		{
			var hlo = _ser.GetAll();
			var category = _categoryService.GetAll();
			ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res = _lostService.GetPostBySpecificUser(userid).FirstOrDefault(x => x.Id == id);
				return View(res);
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpPost("EditLost")]
		public IActionResult EditLost(UpdateLostDto update)
		{

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res = _lostService.UpdateLost(update, userid);
				return RedirectToAction("LostPosts");
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpGet("DeleteLost")]
		public IActionResult DeleteLost(Guid id)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res = _lostService.DeleteLost(id, userid);
				return RedirectToAction("LostPosts", "Profile");
			}
			return RedirectToAction("Login", "Auth");
		}
		[HttpGet("DeleteFound")]
		public IActionResult DeleteFound(Guid id)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res = _foundService.DeleteLost(id, userid);
				return RedirectToAction("FoundPost", "Profile");
			}
			return RedirectToAction("Login", "Auth");
		}
	}
}
