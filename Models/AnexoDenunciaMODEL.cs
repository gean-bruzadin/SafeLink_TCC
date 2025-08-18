using System.ComponentModel.DataAnnotations;

namespace SafeLink_TCC.Models
{
    public class AnexoDenunciaMODEL
    {
        [Key]
        public int Id_Anexo { get; set; }
        public string Tipo_Arquivo { get; set; }
        public string Caminho_anexo { get; set; }
    }
}
