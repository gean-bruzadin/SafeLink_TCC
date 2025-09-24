using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    public class DenunciaAnexoMODEL
    {
        [Key]
        public int Id_anexo { get; set; }

        [ForeignKey("Anexo")]
        public int AnexoId { get; set; }
        public AnexoDenunciaMODEL Anexo { get;

        [ForeignKey("Denuncia")]
        public int DenunciaId { get; set; }
        public DenunciaMODEL Denuncia {  get; set; }
    }
}
