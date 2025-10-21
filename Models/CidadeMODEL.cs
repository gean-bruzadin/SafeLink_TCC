using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class CidadeMODEL
    {
        [Key]
        public int Id_Cidade { get; set; }
        [Required]
        public string Nome_cidade { get; set; }

        [ForeignKey("Estado")]
        public int Id_Estado { get; set; }
        public EstadoMODEL Estado { get; set; }

        public ICollection<EscolaMODEL> Escolas { get; set; }

    }
}
