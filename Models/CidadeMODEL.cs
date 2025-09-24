using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class CidadeMODEL
    {
        [Key]
        public int Id_cidade { get; set; }
        [Required]
        public string Nome_cidade { get; set; }

        [ForeignKey("Estado")]
        public int EstadoId { get; set; }
        public EstadoMODEL Estado { get; set; }

    }
}
