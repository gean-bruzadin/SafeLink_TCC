using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class Denuncia_TestemunhaMODEL
    {
        [ForeignKey("Testemunha")]
        public int Id_Testemunha { get; set; }
        public TestemunhaMODEL Testemunha { get; set; }

        [ForeignKey("Denuncia")]
        public int Id_Denuncia { get; set; }
        public DenunciaMODEL Denuncias { get; set; }
    }
}
