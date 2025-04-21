using Microsoft.AspNetCore.Mvc;
using TraceNest.Services.CategoryServices;
using TraceNest.Services.FoundServices;
using TraceNest.Services.LostServices;
using TraceNest.Services.MunicipalityServices;
using TraceNest.Services.UserServices;

namespace TraceNest.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
		private readonly ILostService _services;
		private readonly IFoundService _ser;
		public readonly IMunicipalityService _municipalityService;
		public readonly ICategoryService _categoryService;
		public AdminController(IUserService userService,ILostService _service, IFoundService ser, IMunicipalityService municipalityService, ICategoryService categoryService)
		{
			_userService = userService;
			_services = _service;
			_ser = ser;
			_municipalityService = municipalityService;
			_categoryService = categoryService;
		}
		public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserDetails()
		{
            var res=await _userService.GetAll();
			return View(res);
		}
		public IActionResult LostProducts()
		{
			var res=_services.GetAll();
			return View(res);
		}
		public IActionResult FoundProducts()
		{
			var res= _ser.GetAll();
			return View(res);
		}
		public IActionResult Muncipality()
		{
			var res = _municipalityService.GetAll();
			return View(res);
		}
		public IActionResult Category()
		{
			var res = _categoryService.GetAll();
			return View(res);
		}
	}
}
