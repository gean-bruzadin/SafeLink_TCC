using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class CargoController : Controller
    {
        private readonly DbConfig _dbconfig;

        public CargoController(DbConfig dbConfig)
        {
            _dbconfig = dbConfig;
        }

        // GET: Cargo
        public async Task<IActionResult> Index()
        {
            return View(await _dbconfig.Cargos.ToListAsync());
        }

        // GET: Cargo/Details/5
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null) return NotFound();
            var cargo = await _dbconfig.Cargos.FirstOrDefaultAsync(m => m.Id_Cargo == id);
            if (cargo == null) return NotFound();
            return View(cargo);
        }

        // GET: Cargo/Create
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Cargo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CargoMODEL cargo)
        {


            _dbconfig.Add(cargo);
            await _dbconfig.SaveChangesAsync();


            return View(cargo);
        }

        // GET: Cargo/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();
            var cargo = await _dbconfig.Cargos.FindAsync(id);
            if (cargo == null) return NotFound();
            return View(cargo);
        }

        // POST: Cargo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, CargoMODEL cargo)
        {
            if (id != cargo.Id_Cargo) return NotFound();



            
                _dbconfig.Update(cargo);
                await _dbconfig.SaveChangesAsync();
            
            


            return View(cargo);
        }

        // GET: Cargo/Delete/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null) return NotFound();
            var cargo = await _dbconfig.Cargos.FirstOrDefaultAsync(m => m.Id_Cargo == id);
            if (cargo == null) return NotFound();
            return View(cargo);
        }
    }
}
