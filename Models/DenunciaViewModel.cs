namespace SafeLink_TCC.Models
{
    public class DenunciaViewModel
    {
        public DenunciaMODEL Denuncia { get; set; }
        
        public List<Testemunha_DenunciaMODEL> Testemunhas { get; set; }

        public List<DenunciaAnexoMODEL> Anexos { get; set; }


    }
}
