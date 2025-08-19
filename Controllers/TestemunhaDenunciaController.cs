using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class TestemunhaDenunciaController : Controller
    {
        private readonly DbConfig _dbconfig;

        public TestemunhaDenunciaController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        // Listar todas as testemunhas
        public async Task<IActionResult> Index()
        {
            var testemunhas = await _dbconfig.TestemunhasDenuncia.ToListAsync();
            return View(testemunhas);
        }

        // Detalhes de uma testemunha específica
        public async Task<IActionResult> Detalhes(int id)
        {
            var testemunha = await _dbconfig.TestemunhasDenuncia.FindAsync(id);
            if (testemunha == null) return NotFound();

            return View(testemunha);
        }

        // Formulário de cadastro
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        // Cadastro (POST)
        [HttpPost]
        public async Task<IActionResult> Cadastro(Testemunha_DenunciaMODEL testemunha)
        {
            if (!ModelState.IsValid)
                return View(testemunha);

            await _dbconfig.TestemunhasDenuncia.AddAsync(testemunha);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Editar (GET)
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var testemunha = await _dbconfig.TestemunhasDenuncia.FindAsync(id);
            if (testemunha == null) return NotFound();

            return View(testemunha);
        }

        // Editar (POST)
        [HttpPost]
        public async Task<IActionResult> Editar(Testemunha_DenunciaMODEL testemunha)
        {
            if (!ModelState.IsValid)
                return View(testemunha);

            _dbconfig.TestemunhasDenuncia.Update(testemunha);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Deletar
        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var testemunha = await _dbconfig.TestemunhasDenuncia.FindAsync(id);
            if (testemunha == null) return NotFound();

            _dbconfig.TestemunhasDenuncia.Remove(testemunha);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

