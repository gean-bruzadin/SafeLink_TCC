using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class CargoMODEL
    {
        [Key]
        public int Id_Cargo { get; set; }
        [Required]
        public string nome_cargo { get; set; }
        [Required]

        public ICollection<FuncionarioMODEL> Funcionarios { get; set; }
    }
}
