using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;
using System.Security.Claims;

namespace SafeLink_TCC.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly DbConfig _dbConfig;

        public AutenticacaoController(DbConfig dbconfig)
        {
            _dbConfig = dbconfig;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            // Primeiro, tenta achar um usuário (Funcionario/Admin)
            var usuario = await _dbConfig.Usuarios
                .Include(u => u.Nivel)
                .FirstOrDefaultAsync(u => u.Email_usuario == email);

            if (usuario != null && BCrypt.Net.BCrypt.Verify(senha, usuario.Senha_usuario))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id_usuario.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome_usuario),
            new Claim(ClaimTypes.Email, usuario.Email_usuario),
            new Claim(ClaimTypes.Role, usuario.Nivel.Nome_Nivel)
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties { IsPersistent = true }
                );

                if (usuario.Nivel.Nome_Nivel == "Admin")
                    return RedirectToAction("Listar", "Usuario");
                else if (usuario.Nivel.Nome_Nivel == "Funcionario")
                    return RedirectToAction("Index", "Home");
            }

            // Caso não seja usuario, tenta autenticar como aluno
            var aluno = await _dbConfig.Alunos
                .Include(a => a.Nivel)
                .FirstOrDefaultAsync(a => a.Email_Aluno == email);

            if (aluno != null && BCrypt.Net.BCrypt.Verify(senha, aluno.Senha_Aluno))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, aluno.Id_Aluno.ToString()),
            new Claim(ClaimTypes.Name, aluno.Nome_Aluno),
            new Claim(ClaimTypes.Email, aluno.Email_Aluno),
            new Claim(ClaimTypes.Role, aluno.Nivel.Nome_Nivel) // "Aluno"
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties { IsPersistent = true }
                );

                return RedirectToAction("Index", "Aluno");
            }

            ViewBag.Mensagem = "E-mail ou senha inválidos.";
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