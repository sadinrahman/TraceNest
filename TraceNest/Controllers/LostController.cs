using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TraceNest.Dto;
using TraceNest.Services.CategoryServices;
using TraceNest.Services.LostServices;
using TraceNest.Services.MunicipalityServices;

namespace TraceNest.Controllers
{
	[Route("Lost")]
	public class LostController : Controller
    {
        private readonly ILostService _services;
        private readonly IMunicipalityService _ser;
        private readonly ICategoryService _categoryService;
        public LostController(ILostService service, IMunicipalityService service1, ICategoryService categoryService)
        {
            _services = service;
            _ser = service1;
            _categoryService = categoryService;
        }
        [HttpGet("ReportLost")]

		
		public async Task<IActionResult> ReportLost()
        {
            var res = await _ser.GetAll();
			var category =await _categoryService.GetAll();
            ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(res, "Id", "MunicipalityName");
			return View();
        }
		[Authorize]
        [HttpPost("ReportLost")]
        public async Task<IActionResult> ReportLost([FromForm]LostAddDto dto,IFormFile Photo)
        {
			if (!ModelState.IsValid)
            {
				Console.WriteLine("something fishy");
				foreach (var state in ModelState)
				{
					foreach (var error in state.Value.Errors)
					{
						Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");
					}
				}
				Console.WriteLine("Illaaaaaa");
				var hlo = await _ser.GetAll();
				var category =await  _categoryService.GetAll();
				ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
				ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
				return View(dto);
            }
			Console.WriteLine("Unddddddddddddddd");
			if (dto.CategoryId == null && !string.IsNullOrEmpty(dto.CustomCategory))
			{
				Console.WriteLine("Keruniillllllaaaaaaaaa");
				// Save the new category
				var newCategoryId =await _categoryService.AddCategoryAsync(dto.CustomCategory.Trim());
				dto.CategoryId = newCategoryId;
			}
			if (dto.MunicipalityId == null && !string.IsNullOrEmpty(dto.customMunicipality))
			{

				var newCategoryId = await _ser.AddMuncipality(dto.customMunicipality);
				dto.MunicipalityId = newCategoryId;
			}
			
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userId))
			{
				var res =await  _services.PostLostProduct(dto, userId, Photo);
				Console.WriteLine(res);
				return RedirectToAction("Lost", "Lost");
			}
			else
			{
				return RedirectToAction("Login", "Auth");
			}
			
			
		}
		[HttpGet("Lost")]
		public async Task<IActionResult> Lost(Guid? municipalityId, Guid? categoryId)
		{
			var municipalities = await _ser.GetAll();
			var categories = await _categoryService.GetAll();

			ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(municipalities, "Id", "MunicipalityName");

			var items = await _services.GetAll();

			if (municipalityId.HasValue)
			{
				var municipality = await _ser.GetMunicipalityName(municipalityId.Value);
				items = items.Where(x => x.Municipality == municipality).ToList();
			}

			if (categoryId.HasValue)
			{
				var category = await _categoryService.GetCategoryName(categoryId.Value);
				items = items.Where(x => x.Category == category).ToList();
			}

			return View(items);
		}


	}
}
