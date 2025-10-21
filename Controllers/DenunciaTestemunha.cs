using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class DenunciaTestemunha : Controller
    {
        private readonly DbConfig _dbconfig;

        public DenunciaTestemunha(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        // GET: DenunciaTestemunha
        public async Task<IActionResult> Index()
        {
            var denunciaTestemunhas = _dbconfig.DenunciaTestemunhas.Include(d => d.Denuncias).Include(d => d.Testemunha);
            return View(await denunciaTestemunhas.ToListAsync());
        }

        // GET: DenunciaTestemunha/Details?idDenuncia=5&idTestemunha=1
        public async Task<IActionResult> Detalhes(int? idDenuncia, int? idTestemunha)
        {
            if (idDenuncia == null || idTestemunha == null) return NotFound();

            var denunciaTestemunha = await _dbconfig.DenunciaTestemunhas
                .Include(d => d.Denuncias)
                .Include(d => d.Testemunha)
                .FirstOrDefaultAsync(m => m.Id_Denuncia == idDenuncia && m.Id_Testemunha == idTestemunha);

            if (denunciaTestemunha == null) return NotFound();

            return View(denunciaTestemunha);
        }

        // GET: DenunciaTestemunha/Create
        public IActionResult Criar()
        {
            ViewData["Id_Denuncia"] = new SelectList(_dbconfig.Denuncias, "Id_Denuncia", "Descricao_Denuncia");
            ViewData["Id_Testemunha"] = new SelectList(_dbconfig.Testemunhas, "Id_Testemunha", "Nome_Testemunha");
            return View();
        }

        // POST: DenunciaTestemunha/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Denuncia_TestemunhaMODEL denunciaTestemunha)
        {

            _dbconfig.Add(denunciaTestemunha);
            await _dbconfig.SaveChangesAsync();


            ViewData["Id_Denuncia"] = new SelectList(_dbconfig.Denuncias, "Id_Denuncia", "Descricao_Denuncia", denunciaTestemunha.Id_Denuncia);
            ViewData["Id_Testemunha"] = new SelectList(_dbconfig.Testemunhas, "Id_Testemunha", "Nome_Testemunha", denunciaTestemunha.Id_Testemunha);
            return View(denunciaTestemunha);
        }

        // Não há um método de Edit simples para tabelas de junção. A prática comum é Deletar e Criar uma nova associação.
        // A edição direta é complexa porque envolve mudar a chave primária.

        // GET: DenunciaTestemunha/Delete?idDenuncia=5&idTestemunha=1
        public async Task<IActionResult> Deletar(int? idDenuncia, int? idTestemunha)
        {
            if (idDenuncia == null || idTestemunha == null) return NotFound();

            var denunciaTestemunha = await _dbconfig.DenunciaTestemunhas
                .Include(d => d.Denuncias)
                .Include(d => d.Testemunha)
                .FirstOrDefaultAsync(m => m.Id_Denuncia == idDenuncia && m.Id_Testemunha == idTestemunha);

            if (denunciaTestemunha == null) return NotFound();

            return View(denunciaTestemunha);
        }

        // POST: DenunciaTestemunha/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idDenuncia, int idTestemunha)
        {
            var denunciaTestemunha = await _dbconfig.DenunciaTestemunhas.FindAsync(idDenuncia, idTestemunha);
            _dbconfig.DenunciaTestemunhas.Remove(denunciaTestemunha);
            await _dbconfig.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
   
