using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
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
        public IActionResult ReportLost()
        {
            var res =  _ser.GetAll();
			var category = _categoryService.GetAll();
            ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(res, "Id", "MunicipalityName");
			return View();
        }
		[Authorize]
        [HttpPost("ReportLost")]
        public IActionResult ReportLost([FromForm]LostAddDto dto,IFormFile Photo)
        {
			if (!ModelState.IsValid)
            {
				Console.WriteLine("Illaaaaaa");
				var hlo =  _ser.GetAll();
				var category =  _categoryService.GetAll();
				ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
				ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
				return View(dto);
            }
			Console.WriteLine("Unddddddddddddddd");
			//var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userId))
			{
				var res =  _services.PostLostProduct(dto, userId, Photo);
				Console.WriteLine(res);
				return RedirectToAction("Home", "Home");
			}
			else
			{ 
				return Unauthorized(); 
			}
			
			
		}
		[HttpGet("Lost")]
		public   IActionResult Lost()
		{
			var hlo =  _ser.GetAll();
			var category =  _categoryService.GetAll();
			ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
			var res=_services.GetAll();

			return View(res);
		}

	}
}
