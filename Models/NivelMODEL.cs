using System.ComponentModel.DataAnnotations;

namespace SafeLink_TCC.Models
{
    public class NivelMODEL
    {
        [Key]
        public int Id_Nivel { get; set; }
        public string Nome_Nivel { get; set; }
    }
}
