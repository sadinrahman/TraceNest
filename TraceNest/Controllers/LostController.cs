using Microsoft.AspNetCore.Mvc;
using TraceNest.Services.LostServices;

namespace TraceNest.Controllers
{
    public class LostController : Controller
    {
        private readonly ILostService _services;
		public LostController(ILostService service)
		{
            _services = service;
		}
		public IActionResult Index()
        {
            return View();
        }
    }
}
