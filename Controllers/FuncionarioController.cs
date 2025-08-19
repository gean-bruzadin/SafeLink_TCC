using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly DbConfig _dbconfig;

        public FuncionarioController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        // Listar todos os funcionários
        public async Task<IActionResult> Index()
        {
            var funcionarios = await _dbconfig.Funcionarios.ToListAsync();
            return View(funcionarios);
        }

        // Detalhes de um funcionário
        public async Task<IActionResult> Detalhes(int id)
        {
            var funcionario = await _dbconfig.Funcionarios.FindAsync(id);
            if (funcionario == null) return NotFound();

            return View(funcionario);
        }

        // Formulário de cadastro
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        // Cadastro (POST)
        [HttpPost]
        public async Task<IActionResult> Cadastro(FuncionarioMODEL funcionario)
        {
            if (!ModelState.IsValid)
                return View(funcionario);

            await _dbconfig.Funcionarios.AddAsync(funcionario);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Editar (GET)
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var funcionario = await _dbconfig.Funcionarios.FindAsync(id);
            if (funcionario == null) return NotFound();

            return View(funcionario);
        }

        // Editar (POST)
        [HttpPost]
        public async Task<IActionResult> Editar(FuncionarioMODEL funcionario)
        {
            if (!ModelState.IsValid)
                return View(funcionario);

            _dbconfig.Funcionarios.Update(funcionario);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Deletar
        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var funcionario = await _dbconfig.Funcionarios.FindAsync(id);
            if (funcionario == null) return NotFound();

            _dbconfig.Funcionarios.Remove(funcionario);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
