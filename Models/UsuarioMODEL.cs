using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class UsuarioMODEL
    {
        [Key]
        public int Id_usuario { get; set; }

        [Required]
        public string Nome_usuario { get; set; }

        [Required, EmailAddress]
        public string Email_usuario { get; set; }

        // Aqui guardamos o hash da senha (não a senha em texto)
        [Required]
        public string Senha_usuario { get; set; }

        // chave estrangeira de nível
        [ForeignKey("Nivel")]
        public int NivelId { get; set; }
        public NivelMODEL Nivel { get; set; }
    }
}
