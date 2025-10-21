using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class AnexoController : Controller
    {
        private readonly DbConfig _dbconfig;

        public AnexoController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        public async Task<IActionResult> Index()
        {
            var anexos = _dbconfig.Anexos.Include(a => a.Denuncia);
            return View(await anexos.ToListAsync());
        }

        // GET: Anexo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var anexo = await _dbconfig.Anexos
                .Include(a => a.Denuncia)
                .FirstOrDefaultAsync(m => m.Id_Anexo == id);
            if (anexo == null) return NotFound();
            return View(anexo);
        }

        // GET: Anexo/Create
        public IActionResult Criar()
        {
            ViewData["Id_Denuncia"] = new SelectList(_dbconfig.Denuncias, "Id_Denuncia", "Descricao_Denuncia");
            return View();
        }

        // POST: Anexo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(AnexoMODEL anexo)
        {


            _dbconfig.Add(anexo);
            await _dbconfig.SaveChangesAsync();


            ViewData["Id_Denuncia"] = new SelectList(_dbconfig.Denuncias, "Id_Denuncia", "Descricao_Denuncia", anexo.Id_Denuncia);
            return View(anexo);
        }

        // GET: Anexo/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();
            var anexo = await _dbconfig.Anexos.FindAsync(id);
            if (anexo == null) return NotFound();
            ViewData["Id_Denuncia"] = new SelectList(_dbconfig.Denuncias, "Id_Denuncia", "Descricao_Denuncia", anexo.Id_Denuncia);
            return View(anexo);
        }

        // POST: Anexo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AnexoMODEL anexo)
        {
            if (id != anexo.Id_Anexo) return NotFound();

                _dbconfig.Update(anexo);
                await _dbconfig.SaveChangesAsync();
        
            ViewData["Id_Denuncia"] = new SelectList(_dbconfig.Denuncias, "Id_Denuncia", "Descricao_Denuncia", anexo.Id_Denuncia);
            return View(anexo);
        }

        // GET: Anexo/Delete/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null) return NotFound();
            var anexo = await _dbconfig.Anexos
                .Include(a => a.Denuncia)
                .FirstOrDefaultAsync(m => m.Id_Anexo == id);
            if (anexo == null) return NotFound();
            return View(anexo);
        }
    }
}
