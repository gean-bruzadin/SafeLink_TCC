using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SafeLink_TCC.Config;
using System.Security.Claims;

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
            public IActionResult Login(string email, string senha)
            {
                if (email != null && senha != null)
                {
                    var aluno = _dbConfig.Alunos.FirstOrDefault(
                       a => a.Email_Aluno == email && a.Senha_Aluno == senha
                   );
                    if (aluno != null && BCrypt.Net.BCrypt.Verify(senha, aluno.Senha_Aluno))
                    {
                        var regras = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, aluno.Id_Aluno.ToString()),
                        new Claim(ClaimTypes.Name, aluno.Nome_Aluno),
                        new Claim(ClaimTypes.Email, aluno.Email_Aluno)
                    };

                        var regrasIdentity = new ClaimsIdentity(
                            regras,
                            CookieAuthenticationDefaults.AuthenticationScheme
                            );


                        return RedirectToAction("Index", "usuario");

                    }
                    ViewBag.Mensagem = "E-mail e/ou Senha Inválidos";
                }
                ViewBag.Mensagem = "E-mail e/ou Senha devem ser preenchidos";
                return View();


            }
        }
}

