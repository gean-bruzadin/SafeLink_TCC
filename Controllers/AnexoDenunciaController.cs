using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class AnexoDenunciaController : Controller
    {
        private readonly DbConfig _dbconfig;

        public AnexoDenunciaController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        // Listar todos os anexos
        public async Task<IActionResult> Index()
        {
            var anexos = await _dbconfig.AnexosDenuncia.ToListAsync();
            return View(anexos);
        }

        // Detalhes de um anexo específico
        public async Task<IActionResult> Detalhes(int id)
        {
            var anexo = await _dbconfig.AnexosDenuncia.FindAsync(id);
            if (anexo == null) return NotFound();

            return View(anexo);
        }

        // Formulário de cadastro
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        // Cadastro (POST)
        [HttpPost]
        public async Task<IActionResult> Cadastro(AnexoDenunciaMODEL anexo)
        {
            if (!ModelState.IsValid)
                return View(anexo);

            await _dbconfig.AnexosDenuncia.AddAsync(anexo);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Editar (GET)
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var anexo = await _dbconfig.AnexosDenuncia.FindAsync(id);
            if (anexo == null) return NotFound();

            return View(anexo);
        }

        // Editar (POST)
        [HttpPost]
        public async Task<IActionResult> Editar(AnexoDenunciaMODEL anexo)
        {
            if (!ModelState.IsValid)
                return View(anexo);

            _dbconfig.AnexosDenuncia.Update(anexo);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Deletar
        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var anexo = await _dbconfig.AnexosDenuncia.FindAsync(id);
            if (anexo == null) return NotFound();

            _dbconfig.AnexosDenuncia.Remove(anexo);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
