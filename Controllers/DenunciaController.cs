using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Config;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Controllers
{
    public class DenunciaController : Controller
    {
        private readonly DbConfig _dbconfig;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public DenunciaController(DbConfig dbconfig)
        {
            _dbconfig = dbconfig;
        }

        // ==============================================================
        // INDEX - Listagem de denúncias
        // ==============================================================
        public async Task<IActionResult> Index()
        {
            var denuncias = await _dbconfig.Denuncias
                .Include(d => d.Aluno)
                .Include(d => d.Escola)
                .Include(d => d.Anexos)
                .Include(d => d.DenunciaTestemunhas)
                    .ThenInclude(dt => dt.Testemunha)
                .OrderByDescending(d => d.DataCriacao_Denuncia)
                .ToListAsync();

            return View(denuncias);
        }

        // ==============================================================
        // DETALHES
        // ==============================================================
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null) return NotFound();

            var denuncia = await _dbconfig.Denuncias
                .Include(d => d.Aluno)
                .Include(d => d.Escola)
                .Include(d => d.Anexos)
                .Include(d => d.DenunciaTestemunhas)
                    .ThenInclude(dt => dt.Testemunha)
                .FirstOrDefaultAsync(d => d.Id_Denuncia == id);

            if (denuncia == null) return NotFound();

            return View(denuncia);
        }

        // ==============================================================
        // CRIAR (GET)
        // ==============================================================
        [HttpGet]
        public IActionResult Criar()
        {
            ViewData["Id_Aluno"] = new SelectList(_dbconfig.Alunos, "Id_Aluno", "Nome_Auno");
            ViewData["Id_Escola"] = new SelectList(_dbconfig.Escolas, "Id_Escola", "Nome_Escola");
            return View();
        }

        // ==============================================================
        // CRIAR (POST)
        // ==============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(
            [Bind("Descricao_Denuncia,Categoria_Denuncia,Id_Aluno,Id_Escola")] DenunciaMODEL denuncia,
            IFormFile anexoArquivo,
            List<string> Nome_Testemunha,
            List<string> Telefone_Testemunha)
        {
            denuncia.DataCriacao_Denuncia = DateTime.Now;
            denuncia.Status_Denuncia = "Aberta";

            _dbconfig.Add(denuncia);
            await _dbconfig.SaveChangesAsync();

            // Upload de anexo
            if (anexoArquivo != null && anexoArquivo.Length > 0)
            {
                string uploadsPasta = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsPasta))
                    Directory.CreateDirectory(uploadsPasta);

                string nomeArquivoUnico = Guid.NewGuid() + "_" + Path.GetFileName(anexoArquivo.FileName);
                string caminhoArquivo = Path.Combine(uploadsPasta, nomeArquivoUnico);

                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await anexoArquivo.CopyToAsync(fileStream);
                }

                var anexo = new AnexoMODEL
                {
                    Tipo_Arquivo = anexoArquivo.ContentType,
                    Caminho_anexo = "/uploads/" + nomeArquivoUnico,
                    Id_Denuncia = denuncia.Id_Denuncia
                };

                _dbconfig.Anexos.Add(anexo);
                await _dbconfig.SaveChangesAsync();
            }

            // Testemunhas
            if (Nome_Testemunha != null && Nome_Testemunha.Count > 0)
            {
                for (int i = 0; i < Nome_Testemunha.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(Nome_Testemunha[i])) continue;

                    var testemunha = new TestemunhaMODEL
                    {
                        Nome_Testemunha = Nome_Testemunha[i],
                        Telefone_Testemunha = Telefone_Testemunha.ElementAtOrDefault(i)
                    };

                    _dbconfig.Testemunhas.Add(testemunha);
                    await _dbconfig.SaveChangesAsync();

                    var relacao = new Denuncia_TestemunhaMODEL
                    {
                        Id_Denuncia = denuncia.Id_Denuncia,
                        Id_Testemunha = testemunha.Id_Testemunha
                    };
                    _dbconfig.DenunciaTestemunhas.Add(relacao);
                }

                await _dbconfig.SaveChangesAsync();
            }

            TempData["MensagemSucesso"] = "Denúncia registrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // ==============================================================
        // EDITAR (GET)
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var denuncia = await _dbconfig.Denuncias
                .Include(d => d.DenunciaTestemunhas)
                    .ThenInclude(dt => dt.Testemunha)
                .FirstOrDefaultAsync(d => d.Id_Denuncia == id);

            if (denuncia == null) return NotFound();

            ViewData["Id_Aluno"] = new SelectList(_dbconfig.Alunos, "Id_Aluno", "Nome_Auno", denuncia.Id_Aluno);
            ViewData["Id_Escola"] = new SelectList(_dbconfig.Escolas, "Id_Escola", "Nome_Escola", denuncia.Id_Escola);

            return View(denuncia);
        }

        // ==============================================================
        // EDITAR (POST)
        // ==============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(
            int id,
            DenunciaMODEL denunciaAtualizada,
            IFormFile anexoArquivo,
            List<string> Nome_Testemunha,
            List<string> Telefone_Testemunha)
        {
            var denuncia = await _dbconfig.Denuncias
                .Include(d => d.DenunciaTestemunhas)
                    .ThenInclude(dt => dt.Testemunha)
                .FirstOrDefaultAsync(d => d.Id_Denuncia == id);

            if (denuncia == null) return NotFound();

            // Atualiza campos
            denuncia.descricao_denuncia = denunciaAtualizada.descricao_denuncia;
            denuncia.categoria_denuncia = denunciaAtualizada.categoria_denuncia;
            denuncia.Id_Aluno = denunciaAtualizada.Id_Aluno;
            denuncia.Id_Escola = denunciaAtualizada.Id_Escola;
            denuncia.Status_Denuncia = denunciaAtualizada.Status_Denuncia ?? denuncia.Status_Denuncia;

            // Atualiza anexo
            if (anexoArquivo != null && anexoArquivo.Length > 0)
            {
                string uploadsPasta = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsPasta))
                    Directory.CreateDirectory(uploadsPasta);

                string nomeArquivoUnico = Guid.NewGuid() + "_" + Path.GetFileName(anexoArquivo.FileName);
                string caminhoArquivo = Path.Combine(uploadsPasta, nomeArquivoUnico);

                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await anexoArquivo.CopyToAsync(fileStream);
                }

                var novoAnexo = new AnexoMODEL
                {
                    Tipo_Arquivo = anexoArquivo.ContentType,
                    Caminho_anexo = "/uploads/" + nomeArquivoUnico,
                    Id_Denuncia = denuncia.Id_Denuncia
                };

                _dbconfig.Anexos.Add(novoAnexo);
            }

            // Remove testemunhas antigas
            var relacoesAntigas = _dbconfig.DenunciaTestemunhas
                .Where(dt => dt.Id_Denuncia == denuncia.Id_Denuncia)
                .ToList();

            _dbconfig.DenunciaTestemunhas.RemoveRange(relacoesAntigas);
            await _dbconfig.SaveChangesAsync();

            // Adiciona novas testemunhas
            if (Nome_Testemunha != null && Nome_Testemunha.Count > 0)
            {
                for (int i = 0; i < Nome_Testemunha.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(Nome_Testemunha[i])) continue;

                    var novaTestemunha = new TestemunhaMODEL
                    {
                        Nome_Testemunha = Nome_Testemunha[i],
                        Telefone_Testemunha = Telefone_Testemunha.ElementAtOrDefault(i)
                    };

                    _dbconfig.Testemunhas.Add(novaTestemunha);
                    await _dbconfig.SaveChangesAsync();

                    var novaRelacao = new Denuncia_TestemunhaMODEL
                    {
                        Id_Denuncia = denuncia.Id_Denuncia,
                        Id_Testemunha = novaTestemunha.Id_Testemunha
                    };

                    _dbconfig.DenunciaTestemunhas.Add(novaRelacao);
                }
            }

            await _dbconfig.SaveChangesAsync();
            TempData["MensagemSucesso"] = "Denúncia atualizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // ==============================================================
        // DELETAR (GET)
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null) return NotFound();

            var denuncia = await _dbconfig.Denuncias
                .Include(d => d.Aluno)
                .Include(d => d.Escola)
                .FirstOrDefaultAsync(d => d.Id_Denuncia == id);

            if (denuncia == null) return NotFound();

            return View(denuncia);
        }

        // ==============================================================
        // DELETAR (POST)
        // ==============================================================
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            var denuncia = await _dbconfig.Denuncias
                .Include(d => d.DenunciaTestemunhas)
                .FirstOrDefaultAsync(d => d.Id_Denuncia == id);

            if (denuncia != null)
            {
                // Remove vínculos
                if (denuncia.DenunciaTestemunhas.Any())
                    _dbconfig.DenunciaTestemunhas.RemoveRange(denuncia.DenunciaTestemunhas);

                // Remove anexos
                var anexos = _dbconfig.Anexos.Where(a => a.Id_Denuncia == denuncia.Id_Denuncia);
                _dbconfig.Anexos.RemoveRange(anexos);

                _dbconfig.Denuncias.Remove(denuncia);
                await _dbconfig.SaveChangesAsync();
            }

            TempData["MensagemSucesso"] = "Denúncia excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool DenunciaExists(int id)
        {
            return _dbconfig.Denuncias.Any(e => e.Id_Denuncia == id);
        }
    }
}
