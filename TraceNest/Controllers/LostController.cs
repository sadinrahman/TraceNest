using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;
using TraceNest.Dto;
using TraceNest.Models;
using TraceNest.Repository.MunicipalityRepositories;
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
				return RedirectToAction("Home", "Home");
			}
			else
			{
				return RedirectToAction("Login", "Auth");
			}
			
			
		}
		[HttpGet("Lost")]
		public async Task<IActionResult> Lost()
		{
			var hlo =await  _ser.GetAll();
			var category =await  _categoryService.GetAll();
			ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
			var res=await _services.GetAll();

			return View(res);
		}

	}
}
