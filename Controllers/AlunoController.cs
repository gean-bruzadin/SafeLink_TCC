using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class AlunoController : Controller
    {
        private readonly DbConfig _dbconfig;

        public AlunoController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        public async Task<IActionResult> Index()
        {
            var alunos = await _dbconfig.Alunos.ToListAsync();
            return View(alunos);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await _dbconfig.Alunos.FindAsync(id);
            if (aluno == null) return NotFound();
            return View(aluno);
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(AlunoMODEL aluno)
        {
            if (!ModelState.IsValid)
                return View(aluno);

            await _dbconfig.Alunos.AddAsync(aluno);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(AlunoMODEL aluno)
        {
            if (!ModelState.IsValid)
                return View("Editar", aluno);

            _dbconfig.Alunos.Update(aluno);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost] // poderia ser API com [HttpDelete] se for via AJAX
        public async Task<IActionResult> Deletar(int id)
        {
            var aluno = await _dbconfig.Alunos.FindAsync(id);
            if (aluno == null) return NotFound();

            _dbconfig.Alunos.Remove(aluno);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
