using Microsoft.AspNetCore.Mvc;

namespace SinavYonetimUygulamasi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
