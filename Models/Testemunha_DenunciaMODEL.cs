using System.ComponentModel.DataAnnotations;

namespace SafeLink_TCC.Models
{
    public class Testemunha_DenunciaMODEL
    {
        [Key]
        public int Id_Testemunha { get; set; }
        public string Nome_Testemunha { get; set; }
        public string Telefone_Testemunha { get; set; }
    }
}
