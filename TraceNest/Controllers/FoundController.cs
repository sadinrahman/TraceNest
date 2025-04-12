using Microsoft.AspNetCore.Mvc;
using TraceNest.Services.FoundServices;

namespace TraceNest.Controllers
{
    public class FoundController : Controller
    {
        private readonly IFoundService _service;
		public FoundController(IFoundService service)
		{
            _service = service;
		}
		public IActionResult Index()
        {
            return View();
        }
    }
}
