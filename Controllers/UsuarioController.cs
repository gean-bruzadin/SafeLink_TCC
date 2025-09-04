using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;
using BCrypt.Net;

namespace SafeLink_TCC.Controllers
{

    public class UsuarioController : Controller
    {
        private readonly DbConfig _db;

        public UsuarioController(DbConfig db) => _db = db;

        // GET: /Usuario/Cadastrar
        public IActionResult Cadastrar()
        {
            ViewBag.Niveis = _db.Niveis.ToList(); // dropdown de níveis
            return View();
        }

        // POST: /Usuario/Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(string Nome_usuario, string Email_usuario, string Senha_usuario, int NivelId)
        {
            if (string.IsNullOrWhiteSpace(Nome_usuario) ||
                string.IsNullOrWhiteSpace(Email_usuario) ||
                string.IsNullOrWhiteSpace(Senha_usuario))
            {
                ViewBag.Mensagem = "Todos os campos são obrigatórios.";
                ViewBag.Niveis = _db.Niveis.ToList();
                return View();
            }

            // Checa se o email já existe
            if (await _db.Usuarios.AnyAsync(u => u.Email_usuario == Email_usuario))
            {
                ViewBag.Mensagem = "E-mail já cadastrado.";
                ViewBag.Niveis = _db.Niveis.ToList();
                return View();
            }

            var nivel = await _db.Niveis.FirstOrDefaultAsync(n => n.Id_Nivel == NivelId);
            if (nivel == null)
            {
                ViewBag.Mensagem = "Nível inválido.";
                ViewBag.Niveis = _db.Niveis.ToList();
                return View();
            }

            var usuario = new UsuarioMODEL
            {
                Nome_usuario = Nome_usuario,
                Email_usuario = Email_usuario,
                Senha_usuario = BCrypt.Net.BCrypt.HashPassword(Senha_usuario),
                NivelId = nivel.Id_Nivel
            };

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            TempData["Sucesso"] = "Usuário cadastrado com sucesso!";
            return RedirectToAction("Cadastrar");
        }
    }
}
