using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class EscolaMODEL
    {
        [Key]
        public int Id_Escola { get; set; }
        [Required]
        public string nome_escola { get; set; }
        [Required]
        public string cnpj_escola { get; set; }
        [Required]
        public string endereco_escola { get; set; }
        [Required]
        public string telefone_escola { get; set; }

        [ForeignKey("Cidade")]
        public int Id_Cidade { get; set; }
        public CidadeMODEL Cidade { get; set; }
    }
}
