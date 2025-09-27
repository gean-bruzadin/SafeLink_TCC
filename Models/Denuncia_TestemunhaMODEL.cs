using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class Denuncia_TestemunhaMODEL
    {
        [Key]
        public int Id_Denuncia_testemunha { get; set; }

        [ForeignKey("Testemunha_Denuncia")]
        public int Id_Testemunha { get; set; }
        public Testemunha_DenunciaMODEL Testemunha { get; set; }

        [ForeignKey("Denuncia")]
        public int Id_Denuncia { get; set; }

        public DenunciaMODEL Denuncia { get; set; }
    }
}
