using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class EstadoController : Controller
    {
        private readonly DbConfig _dbconfig;

        public EstadoController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        // Listar todos os estados
        public async Task<IActionResult> Index()
        {
            var estados = await _dbconfig.Estados.ToListAsync();
            return View(estados);
        }

        // Detalhes de um estado
        public async Task<IActionResult> Detalhes(int id)
        {
            var estado = await _dbconfig.Estados.FindAsync(id);
            if (estado == null) return NotFound();

            return View(estado);
        }

        // Formulário de cadastro
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        // Cadastro (POST)
        [HttpPost]
        public async Task<IActionResult> Cadastro(EstadoMODEL estado)
        {
            if (!ModelState.IsValid)
                return View(estado);

            await _dbconfig.Estados.AddAsync(estado);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Editar (GET)
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var estado = await _dbconfig.Estados.FindAsync(id);
            if (estado == null) return NotFound();

            return View(estado);
        }

        // Editar (POST)
        [HttpPost]
        public async Task<IActionResult> Editar(EstadoMODEL estado)
        {
            if (!ModelState.IsValid)
                return View(estado);

            _dbconfig.Estados.Update(estado);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Deletar
        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var estado = await _dbconfig.Estados.FindAsync(id);
            if (estado == null) return NotFound();

            _dbconfig.Estados.Remove(estado);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
