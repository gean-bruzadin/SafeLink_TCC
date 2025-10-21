using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class AnexoMODEL
    {
        [Key]
        public int Id_Anexo { get; set; }
        public string Tipo_Arquivo { get; set; }
        public string Caminho_anexo { get; set; }

        [ForeignKey("Denuncia")]
        public int Id_Denuncia { get; set; }
        public DenunciaMODEL Denuncia { get; set; }
    }
}
