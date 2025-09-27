using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class DenunciaController : Controller
    {
        private readonly DbConfig _dbconfig;


    public DenunciaController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        // Página inicial ou lista de denúncias
        public async Task<IActionResult> Index()
        {
            var denuncias = await _dbconfig.Denuncia.ToListAsync();
            return View(denuncias);
        }

        // Página de cadastro
        public IActionResult Cadastrar()
        {
            return View("CadastrarDenuncia");
        }

        // Salvar nova denúncia
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(DenunciaMODEL denuncia)
        {
            if (!ModelState.IsValid)
                return View("CadastrarDenuncia", denuncia);

            try
            {
                await _dbconfig.Denuncia.AddAsync(denuncia);
                await _dbconfig.SaveChangesAsync();

                TempData["Sucesso"] = "Denúncia registrada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao registrar denúncia: " + ex.Message;
                return View("CadastrarDenuncia", denuncia);
            }
        }

        // Editar denúncia
        public async Task<IActionResult> Editar(int id)
        {
            var denuncia = await _dbconfig.Denuncia.FindAsync(id);
            if (denuncia == null)
                return NotFound();

            return View("CadastrarDenuncia", denuncia);
        }

        // Atualizar denúncia
        [HttpPost]
        public async Task<IActionResult> Atualizar(DenunciaMODEL denuncia)
        {
            if (!ModelState.IsValid)
                return View("CadastrarDenuncia", denuncia);

            try
            {
                _dbconfig.Denuncia.Update(denuncia);
                await _dbconfig.SaveChangesAsync();

                TempData["Sucesso"] = "Denúncia atualizada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao atualizar denúncia: " + ex.Message;
                return View("CadastrarDenuncia", denuncia);
            }
        }

        // Deletar denúncia
        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var denuncia = await _dbconfig.Denuncia.FindAsync(id);
                if (denuncia == null)
                    return NotFound();

                _dbconfig.Denuncia.Remove(denuncia);
                await _dbconfig.SaveChangesAsync();

                TempData["Sucesso"] = "Denúncia deletada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao deletar denúncia: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Lista de denúncias
        public async Task<IActionResult> Listar()
        {
            var denuncias = await _dbconfig.Denuncia.ToListAsync();
            return View("ListarDenuncia", denuncias);
        }
    }


}
