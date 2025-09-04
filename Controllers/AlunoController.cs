using SafeLink_TCC.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Models;
using BCrypt.Net; // precisa do pacote BCrypt.Net-Next

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

        // Salvar novo aluno (com senha criptografada)
        [HttpPost]
        public async Task<IActionResult> Cadastrar(AlunoMODEL aluno)
        {
            if (!ModelState.IsValid)
                return View("CadastrarAluno", aluno);

            // Criptografa senha
            aluno.Senha_Aluno = BCrypt.Net.BCrypt.HashPassword(aluno.Senha_Aluno);

            // Atribui automaticamente o nível "Aluno"
            var nivelAluno = await _dbconfig.Niveis.FirstOrDefaultAsync(n => n.Nome_Nivel == "Aluno");
            if (nivelAluno != null)
            {
                aluno.NivelId = nivelAluno.Id_Nivel;
            }

            await _dbconfig.Alunos.AddAsync(aluno);
            await _dbconfig.SaveChangesAsync();

            TempData["Sucesso"] = "Cadastro realizado com sucesso!";
            return RedirectToAction("Cadastrar");
        }
        // Atualizar aluno (criptografando caso senha tenha sido alterada)
        [HttpPost]
        public async Task<IActionResult> Atualizar(AlunoMODEL aluno)
        {
            if (!ModelState.IsValid)
                return View("CadastrarAluno", aluno);

            // Se a senha não estiver vazia, criptografa novamente
            if (!string.IsNullOrWhiteSpace(aluno.Senha_Aluno))
            {
                aluno.Senha_Aluno = BCrypt.Net.BCrypt.HashPassword(aluno.Senha_Aluno);
            }

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
