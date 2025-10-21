using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class CidadeController : Controller
    {
        private readonly DbConfig _dbconfig;

        public CidadeController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        // GET: Cidade
        public async Task<IActionResult> Index()
        {
            var cidades = _dbconfig.Cidades.Include(c => c.Estado);
            return View(await cidades.ToListAsync());
        }

        // GET: Cidade/Details/5
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null) return NotFound();
            var cidade = await _dbconfig.Cidades
                .Include(c => c.Estado)
                .FirstOrDefaultAsync(m => m.Id_Cidade == id);
            if (cidade == null) return NotFound();
            return View(cidade);
        }

        // GET: Cidade/Create
        public IActionResult Criar()
        {
            ViewData["Id_Estado"] = new SelectList(_dbconfig.Estados, "Id_Estado", "Nome_Estado");
            return View();
        }

        // POST: Cidade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CidadeMODEL cidade)
        {


            _dbconfig.Add(cidade);
            await _dbconfig.SaveChangesAsync();


            ViewData["Id_Estado"] = new SelectList(_dbconfig.Estados, "Id_Estado", "Nome_Estado", cidade.Id_Estado);
            return View(cidade);
        }

        // GET: Cidade/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();
            var cidade = await _dbconfig.Cidades.FindAsync(id);
            if (cidade == null) return NotFound();
            ViewData["Id_Estado"] = new SelectList(_dbconfig.Estados, "Id_Estado", "Nome_Estado", cidade.Id_Estado);
            return View(cidade);
        }

        // POST: Cidade/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, CidadeMODEL cidade)
        {
            if (id != cidade.Id_Cidade) return NotFound();


            
                _dbconfig.Update(cidade);
                await _dbconfig.SaveChangesAsync();
            
            

            ViewData["Id_Estado"] = new SelectList(_dbconfig.Estados, "Id_Estado", "Nome_Estado", cidade.Id_Estado);
            return View(cidade);
        }

        // GET: Cidade/Delete/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null) return NotFound();
            var cidade = await _dbconfig.Cidades
                .Include(c => c.Estado)
                .FirstOrDefaultAsync(m => m.Id_Cidade == id);
            if (cidade == null) return NotFound();
            return View(cidade);
        }
    }
}
