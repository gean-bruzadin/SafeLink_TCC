using System.ComponentModel.DataAnnotations;

namespace SafeLink_TCC.Models
{
    public class EstadoMODEL
    {
        [Key]
        public int Id_Estado { get; set; }
        public string Nome_Estado { get; set; }
        public string Sigla_Estado { get; set; }
    }
}
