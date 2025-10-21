using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class RespostaMODEL
    {
        [Key]
        public int Id_Resposta { get; set; }

        public string Descricao_Resposta { get; set; }

        [ForeignKey("Denuncia")]
        public int Id_Denuncia { get; set; }

        // CORRIGIDO: Propriedade de navegação no singular
        public DenunciaMODEL Denuncia { get; set; }
    }
}
