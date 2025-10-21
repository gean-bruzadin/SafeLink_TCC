using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class FuncionarioMODEL
    {
        [Key]
        public int Id_Funcionario { get; set; }
        public string Cargo_Funcionario { get; set; }
        public string Nome_Funcionario { get; set; }
        public string Departamento_Funcionario { get; set; }

        [ForeignKey("Cargo")]
        public int Id_Cargo { get; set; }
        public CargoMODEL Cargo { get; set; }
    }
}
