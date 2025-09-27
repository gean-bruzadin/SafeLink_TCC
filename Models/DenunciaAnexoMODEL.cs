using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class DenunciaAnexoMODEL
    {
        [Key]
        public int Id_DenunciaAnexo { get; set; }

        // Chave Estrangeira para Anexo
        [ForeignKey("Anexo")]
        public int Id_Anexo  { get; set; }
        public AnexoDenunciaMODEL Anexo { get; set; } // <--- CORRIGIDO AQUI!

        // Chave Estrangeira para Denuncia
        [ForeignKey("Denuncia")]
        public int Id_Denuncia { get; set; }
        public DenunciaMODEL Denuncia { get; set; }
    }
}