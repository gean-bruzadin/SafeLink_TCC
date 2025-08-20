using SafeLink_TCC.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Página inicial ou lista de alunos
        public async Task<IActionResult> Index()
        {
            var alunos = await _dbconfig.Alunos.ToListAsync();
            return View(alunos);
        }

        // Página de cadastro
        public IActionResult Cadastrar()
        {
            return View("CadastrarAluno");
        }

        // Salvar novo aluno
        [HttpPost]
        public async Task<IActionResult> Cadastrar(AlunoMODEL aluno)
        {
            if (!ModelState.IsValid)
                return View("CadastrarAluno", aluno);

            await _dbconfig.Alunos.AddAsync(aluno);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Cadastrar");
        }

        // Editar aluno
        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await _dbconfig.Alunos.FindAsync(id);
            if (aluno == null)
                return NotFound();

            return View("CadastrarAluno", aluno);
        }

        // Atualizar aluno
        [HttpPost]
        public async Task<IActionResult> Atualizar(AlunoMODEL aluno)
        {
            if (!ModelState.IsValid)
                return View("CadastrarAluno", aluno);

            _dbconfig.Alunos.Update(aluno);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Cadastrar");
        }

        // Deletar aluno
        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var aluno = await _dbconfig.Alunos.FindAsync(id);
            if (aluno == null)
                return NotFound();

            _dbconfig.Alunos.Remove(aluno);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Lista de alunos (opcional)
        public async Task<IActionResult> Listar()
        {
            var alunos = await _dbconfig.Alunos.ToListAsync();
            return View(alunos);
        }
    }
}
