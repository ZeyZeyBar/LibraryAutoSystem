using Microsoft.AspNetCore.Mvc;

namespace LibraryAutoSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
