using SafeLink_TCC.Controllers;
using System.ComponentModel.DataAnnotations;

namespace SafeLink_TCC.Models
{
    public class TestemunhaMODEL
    {
        [Key]
        public int Id_Testemunha { get; set; }
        public string Nome_Testemunha { get; set; }
        public string Telefone_Testemunha { get; set; }

        public ICollection<Denuncia_TestemunhaMODEL> DenunciaTestemunhas { get; set; }
    }
}
