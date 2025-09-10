using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class AlunoMODEL
    {
        [Key]
        public int Id_Aluno { get; set; }
        [Required]
        public string Nome_Aluno { get; set; }
        [Required]
        public string Email_Aluno { get; set; } 
        [Required]
        public string Senha_Aluno { get; set; }

        // Alterado para corresponder ao nome da coluna no banco de dados
        public int Id_Nivel { get; set; }

        [ForeignKey("Id_Nivel")] // A anotação agora usa o nome correto da coluna
        public NivelMODEL Nivel { get; set; }
    }
}