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

namespace TraceNest.Controllers
{
	[Route("Found")]
	public class FoundController : Controller
	{
		private readonly IFoundService _services;
		private readonly IMunicipalityService _ser;
		private readonly ICategoryService _categoryService;
		public FoundController(IFoundService service, IMunicipalityService service1, ICategoryService categoryService)
		{
			_services = service;
			_ser = service1;
			_categoryService = categoryService;
		}
		[HttpGet("ReportFound")]
		public async Task<IActionResult> ReportFound()
		{
			Console.WriteLine("hiiiiiiiiiiiiiii");
			var res =await _ser.GetAll();
			var category =await _categoryService.GetAll();
			ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(res, "Id", "MunicipalityName");
			return View();
		}
		[Authorize]
		[HttpPost("ReportFound")]
		public async Task<IActionResult> ReportFound([FromForm] FoundPostDto dto, IFormFile Photo)
		{
			Console.WriteLine("Hlooooooooooooooooooooo");
			if (!ModelState.IsValid)
			{
				foreach (var state in ModelState)
				{
					foreach (var error in state.Value.Errors)
					{
						Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");
					}
				}

				var hlo = await _ser.GetAll();
				var category = await _categoryService.GetAll();
				ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
				ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
				return View(dto);
			}
			if (dto.CategoryId == null && !string.IsNullOrEmpty(dto.CustomCategory))
			{
				var newCategoryId =await _categoryService.AddCategoryAsync(dto.CustomCategory);
				dto.CategoryId = newCategoryId;
			}
			if (dto.MunicipalityId == null && !string.IsNullOrEmpty(dto.customMunicipality))
			{
				
				var newCategoryId =await _ser.AddMuncipality(dto.customMunicipality);
				dto.MunicipalityId = newCategoryId;	
			}
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userId))
			{
				var res =await _services.PostFoundProduct(dto, userId, Photo);
				return RedirectToAction("Found", "Found");
			}
			else
			{
				return RedirectToAction("Login", "Auth");
			}
		}
		[HttpGet("Found")]
		public async Task<IActionResult> Found(Guid? municipalityId, Guid? categoryId)
		{
			var municipalities = await _ser.GetAll();
			var categories = await _categoryService.GetAll();

			ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName", categoryId);
			ViewBag.MunicipalityList = new SelectList(municipalities, "Id", "MunicipalityName", municipalityId);

			var items = await _services.GetAll();

			if (municipalityId.HasValue)
			{
				var muncipality=await _ser.GetMunicipalityName(municipalityId.Value);
				items = items.Where(i => i.Municipality == muncipality).ToList();
			}
			if (categoryId.HasValue)
			{
				var category = await _categoryService.GetCategoryName(categoryId.Value);
				items = items.Where(i => i.Category == category).ToList();
			}
			return View(items);
		}

	}
}
