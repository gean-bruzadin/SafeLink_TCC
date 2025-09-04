using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class AlunoMODEL
    {
        [Key]
        public int Id_Aluno { get; set; }
        public string Nome_Aluno { get; set; }
        public string Email_Aluno { get; set; }
        public string Senha_Aluno { get; set; }


        public int NivelId { get; set; }

        [ForeignKey("NivelId")]
        public NivelMODEL Nivel { get; set; }
    }
}
