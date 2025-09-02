using Microsoft.AspNetCore.Mvc;

namespace SafeLink_TCC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
