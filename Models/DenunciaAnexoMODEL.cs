using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class DenunciaAnexoMODEL
    {
        [Key]
        public int Id_anexo { get; set; }

        // Chave Estrangeira para Anexo
        [ForeignKey("Anexo")]
        public int AnexoId { get; set; }
        public AnexoDenunciaMODEL Anexo { get; set; } // <--- CORRIGIDO AQUI!

        // Chave Estrangeira para Denuncia
        [ForeignKey("Denuncia")]
        public int DenunciaId { get; set; }
        public DenunciaMODEL Denuncia { get; set; }
    }
}