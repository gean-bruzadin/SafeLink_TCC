using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SafeLink_TCC.Config;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Login(string Email_Aluno, string Senha_Aluno)
        {
            if (string.IsNullOrWhiteSpace(Email_Aluno) || string.IsNullOrWhiteSpace(Senha_Aluno))
            {
                ViewBag.Mensagem = "E-mail e/ou Senha devem ser preenchidos";
                return View();
            }

            var aluno = await _dbConfig.Alunos
                .FirstOrDefaultAsync(a => a.Email_Aluno == Email_Aluno);

            if (aluno != null && BCrypt.Net.BCrypt.Verify(Senha_Aluno, aluno.Senha_Aluno))
            {
                // Criar Claims com dados do usuário
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
                    IsPersistent = true // mantém login após fechar navegador
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                // Redirecionar para Home/Index após login
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensagem = "E-mail e/ou Senha Inválidos";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
