using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class CargoMODEL
    {
        [Key]
        public int Id_cargo { get; set; }
        [Required]
        public string nome_cargo { get; set; }
        [Required]

        [ForeignKey("Funcionario")]
        public int FuncionarioId { get; set; }
        public FuncionarioMODEL Funcionario { get; set; }
    }
}
