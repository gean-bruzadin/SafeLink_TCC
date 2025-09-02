using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SafeLink_TCC.Controllers
{
    [Authorize] // protege a Home — apenas usuários autenticados acessam.
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // A View pode ler os claims direto do `User`.
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
