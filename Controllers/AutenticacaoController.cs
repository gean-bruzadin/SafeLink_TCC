using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SafeLink_TCC.Config;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SafeLink_TCC.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly DbConfig _dbConfig;

        public AutenticacaoController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                ViewBag.Mensagem = "E-mail e/ou Senha devem ser preenchidos";
                return View();
            }

            var aluno = _dbConfig.Alunos.FirstOrDefault(
                a => a.Email_Aluno == email
            );

            if (aluno != null && BCrypt.Net.BCrypt.Verify(senha, aluno.Senha_Aluno))
            {
                // Criar Claims com nome e id do usuário
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, aluno.Id_Aluno.ToString()),
                    new Claim(ClaimTypes.Name, aluno.Nome_Aluno),
                    new Claim(ClaimTypes.Email, aluno.Email_Aluno)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // mantém o login após fechar o navegador
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                // Redirecionar para Home/Index
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensagem = "E-mail e/ou Senha Inválidos";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
