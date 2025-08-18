using System.ComponentModel.DataAnnotations;

namespace SafeLink_TCC.Models
{
    public class AlunoMODEL
    {
        [Key]
        public int Id_Aluno { get; set; }
        public string Nome_Aluno { get; set; }
        public string Email_Aluno { get; set; }
        public string Senha_Aluno { get; set; }

    }
}
