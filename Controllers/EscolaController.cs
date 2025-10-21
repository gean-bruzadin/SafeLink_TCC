using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class EscolaController : Controller
    {
        private readonly DbConfig _dbconfig;

        public EscolaController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        // GET: Escola
        public async Task<IActionResult> Index()
        {
            var escolas = _dbconfig.Escolas.Include(e => e.Cidade).Include(e => e.Funcionario);
            return View(await escolas.ToListAsync());
        }

        // GET: Escola/Details/5
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null) return NotFound();
            var escola = await _dbconfig.Escolas
                .Include(e => e.Cidade)
                .Include(e => e.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_Escola == id);
            if (escola == null) return NotFound();
            return View(escola);
        }

        // GET: Escola/Create
        public IActionResult Criar()
        {
            ViewData["Id_Cidade"] = new SelectList(_dbconfig.Cidades, "Id_Cidade", "Nome_Cidade");
            ViewData["Id_Funcionario"] = new SelectList(_dbconfig.Funcionarios, "Id_Funcionario", "Nome_Funcionario");
            return View();
        }

        // POST: Escola/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(EscolaMODEL escola)
        {


            _dbconfig.Add(escola);
            await _dbconfig.SaveChangesAsync();

            ViewData["Id_Cidade"] = new SelectList(_dbconfig.Cidades, "Id_Cidade", "Nome_Cidade", escola.Id_Cidade);
            ViewData["Id_Funcionario"] = new SelectList(_dbconfig.Funcionarios, "Id_Funcionario", "Nome_Funcionario", escola.Id_Funcionario);
            return View(escola);
        }

        // GET: Escola/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();
            var escola = await _dbconfig.Escolas.FindAsync(id);
            if (escola == null) return NotFound();
            ViewData["Id_Cidade"] = new SelectList(_dbconfig.Cidades, "Id_Cidade", "Nome_Cidade", escola.Id_Cidade);
            ViewData["Id_Funcionario"] = new SelectList(_dbconfig.Funcionarios, "Id_Funcionario", "Nome_Funcionario", escola.Id_Funcionario);
            return View(escola);
        }

        // POST: Escola/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, EscolaMODEL escola)
        {
            if (id != escola.Id_Escola) return NotFound();


            try
            {
                _dbconfig.Update(escola);
                await _dbconfig.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EscolaExists(escola.Id_Escola)) return NotFound();
                else throw;
            }

            ViewData["Id_Cidade"] = new SelectList(_dbconfig.Cidades, "Id_Cidade", "Nome_Cidade", escola.Id_Cidade);
            ViewData["Id_Funcionario"] = new SelectList(_dbconfig.Funcionarios, "Id_Funcionario", "Nome_Funcionario", escola.Id_Funcionario);
            return View(escola);
        }

        // GET: Escola/Delete/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null) return NotFound();
            var escola = await _dbconfig.Escolas
                .Include(e => e.Cidade)
                .Include(e => e.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_Escola == id);
            if (escola == null) return NotFound();
            return View(escola);
        }

        // POST: Escola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escola = await _dbconfig.Escolas.FindAsync(id);
            _dbconfig.Escolas.Remove(escola);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscolaExists(int id)
        {
            return _dbconfig.Escolas.Any(e => e.Id_Escola == id);
        }
    }
}
