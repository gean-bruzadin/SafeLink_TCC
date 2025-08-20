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

        // Lista todos os alunos
        public async Task<IActionResult> Index()
        {
            var alunos = await _dbconfig.Alunos.ToListAsync();
            return View(alunos); // View: Views/Aluno/Index.cshtml
        }

        // Exibe o formulário de cadastro
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View("CadastrarAluno"); // Especifica a view CadastrarAluno.cshtml
        }

        // Recebe os dados do formulário de cadastro
        [HttpPost]
        public async Task<IActionResult> Cadastro(AlunoMODEL aluno)
        {
            if (!ModelState.IsValid)
                return View("CadastrarAluno", aluno); // Reexibe a view com dados e erros

            await _dbconfig.Alunos.AddAsync(aluno);
            await _dbconfig.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Exibe o formulário de edição
        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await _dbconfig.Alunos.FindAsync(id);
            if (aluno == null) return NotFound();

            return View(aluno); // View: Views/Aluno/Editar.cshtml
        }

        // Recebe os dados do formulário de edição
        [HttpPost]
        public async Task<IActionResult> Atualizar(AlunoMODEL aluno)
        {
            if (!ModelState.IsValid)
                return View("Editar", aluno); // Reexibe a view com erros

            _dbconfig.Alunos.Update(aluno);
            await _dbconfig.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Deleta um aluno pelo ID
        [HttpPost]
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
