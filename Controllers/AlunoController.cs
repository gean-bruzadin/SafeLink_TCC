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

        // Salvar novo aluno (com senha criptografada)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(AlunoMODEL aluno)
        {
          
            

            // Testa conexão com o DB (opcional, ajuda a diagnosticar)
            if (!await _dbconfig.Database.CanConnectAsync())
            {
                ModelState.AddModelError("", "Não foi possível conectar ao banco de dados. Verifique a conexão.");
                return View("CadastrarAluno", aluno);
            }

            try
            {
                // Criptografa a senha antes de salvar
                aluno.Senha_Aluno = BCrypt.Net.BCrypt.HashPassword(aluno.Senha_Aluno);

                // Define o nível automaticamente como Aluno (ID = 1)
                aluno.Id_Nivel = 1; // LINHA ALTERADA

                await _dbconfig.Alunos.AddAsync(aluno);
                await _dbconfig.SaveChangesAsync();

                TempData["Sucesso"] = "Cadastro realizado com sucesso!";
                return RedirectToAction("Login", "Autenticacao");
            }
            catch (Exception ex)
            {
                // Log: você pode injetar ILogger<AlunoController> para registrar melhor
                TempData["Erro"] = "Ocorreu um erro ao salvar o cadastro: " + ex.Message;
                ModelState.AddModelError("", "Erro ao cadastrar: " + ex.Message);
                return View("CadastrarAluno", aluno);
            }
        }

        // Editar aluno
        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await _dbconfig.Alunos.FindAsync(id);
            if (aluno == null)
                return NotFound();

            return View("CadastrarAluno", aluno);
        }

        // Atualizar aluno (criptografando caso senha tenha sido alterada)
        [HttpPost]
        public async Task<IActionResult> Atualizar(AlunoMODEL aluno)
        {
            if (!ModelState.IsValid)
                return View("CadastrarAluno", aluno);

            try
            {
                // Se a senha não estiver vazia, criptografa novamente
                if (!string.IsNullOrWhiteSpace(aluno.Senha_Aluno))
                {
                    aluno.Senha_Aluno = BCrypt.Net.BCrypt.HashPassword(aluno.Senha_Aluno);
                }

                _dbconfig.Alunos.Update(aluno);
                await _dbconfig.SaveChangesAsync();

                TempData["Sucesso"] = "Aluno atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao atualizar aluno: " + ex.Message;
                return View("CadastrarAluno", aluno);
            }
        }

        // Deletar aluno
        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var aluno = await _dbconfig.Alunos.FindAsync(id);
                if (aluno == null)
                    return NotFound();

                _dbconfig.Alunos.Remove(aluno);
                await _dbconfig.SaveChangesAsync();

                TempData["Sucesso"] = "Aluno deletado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao deletar aluno: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Lista de alunos (opcional)
        public async Task<IActionResult> Listar()
        {
            var alunos = await _dbconfig.Alunos.ToListAsync();
            return View("ListarAluno",alunos);
        }
    }
}
