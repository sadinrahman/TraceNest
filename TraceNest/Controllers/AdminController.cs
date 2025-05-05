using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TraceNest.Dto;
using TraceNest.Models;
using TraceNest.Services.CategoryServices;
using TraceNest.Services.FoundServices;
using TraceNest.Services.LostServices;
using TraceNest.Services.MunicipalityServices;
using TraceNest.Services.UserServices;

namespace TraceNest.Controllers
{
	[Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
		private readonly ILostService _services;
		private readonly IFoundService _ser;
		public readonly IMunicipalityService _municipalityService;
		public readonly ICategoryService _categoryService;
		public AdminController(IUserService userService,ILostService service, IFoundService ser, IMunicipalityService municipalityService, ICategoryService categoryService)
		{
			_userService = userService;
			_services = service;
			_ser = ser;
			_municipalityService = municipalityService;
			_categoryService = categoryService;
		}
		public async Task<IActionResult> Index()
        {
			var users =await _userService.Countusers();
			var found =await _ser.CountFound();
			var lost =await _services.LostCount();
			var category= await _categoryService.CategoryCount();
			var muncipality=await _municipalityService.muncipalitycount();
			var result = new DashboardDto
			{
				UsersCount = users,
				FoundItemsCount = found,
				LostItemsCount = lost,
				CategoriesCount = category,
				MunicipalitiesCount = muncipality
			};
			return View(result);
        }
		[HttpGet("UserDetails")]
        public async Task<IActionResult> UserDetails()
		{
            var res=await _userService.GetAll();
			return View(res);
		}
		public async Task<IActionResult> LostProducts()
		{
			var res=await _services.GetAll();
			return View(res);
		}
		public async Task<IActionResult> FoundProducts()
		{
			var res=await _ser.GetAll();
			return View(res);
		}
		public async Task<IActionResult> Muncipality()
		{
			var res =await _municipalityService.GetAll();
			return View(res);
		}
		public async Task<IActionResult> Category()
		{
			var res =await _categoryService.GetAll();
			return View(res);
		}
		[HttpPost("UserDetails")]
		public async Task<IActionResult> UserDetails(Guid id)
		{
			var res=await _userService.BlockUnblock(id);
			if (res)
			{
				return RedirectToAction("UserDetails");
			}
			else
			{
				return View("Error");
			}
		}
		[HttpPost("DeleteCategory")]
		public async Task<IActionResult> DeleteCategory(Guid id)
		{
			var res =await _categoryService.RemoveCategory(id);
			if (res)
			{
				return RedirectToAction("Category");
			}
			else
			{
				return View("Error");
			}
		}
		[HttpPost("DeleteMunicipality")]
		public async Task<IActionResult> DeleteMunicipality(Guid id)
		{
			var res =await _municipalityService.RemoveMuncipality(id);
			if (res)
			{
				return RedirectToAction("Muncipality");
			}
			else
			{
				return View("Error");
			}
		}
		[HttpPost("EditMunicipality")]
		public async Task<IActionResult> EditMunicipality(MuncipalityDto municipality)
		{
			var res =await _municipalityService.UpdateMunicipality(municipality);
			return RedirectToAction("Muncipality");
		}
		[HttpPost("EditCategory")]
		public async Task<IActionResult> EditCategory(CategoryDto municipality)
		{
			var res =await _categoryService.UpdateMunicipality(municipality);
			return RedirectToAction("Category");
		}
		[HttpPost("AddMunicipality")]
		public async Task<IActionResult> AddMunicipality(string municipality)
		{
			var res =await _municipalityService.AddMuncipality(municipality);
			return RedirectToAction("Muncipality");
		}
		[HttpPost("AddCategory")]
		public async Task<IActionResult> AddCategory(string category)
		{
			var res =await _categoryService.AddCategoryAsync(category);
			return RedirectToAction("Category");
		}
	}
}
