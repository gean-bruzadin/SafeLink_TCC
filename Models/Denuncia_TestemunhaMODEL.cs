using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class Denuncia_TestemunhaMODEL
    {
        [Key]
        public int Id_Denuncia_testemunha { get; set; }

        [ForeignKey("Testemunha")]
        public int TestemunhaId { get; set; }
        public Testemunha_DenunciaMODEL Testemunha { get; set; }

        [ForeignKey("Denuncia")]
        public int DenunciaId { get; set; }

        public DenunciaMODEL Denuncia { get; set; }
    }
}
