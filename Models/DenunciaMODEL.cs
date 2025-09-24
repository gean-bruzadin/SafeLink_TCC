using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeLink_TCC.Models
{
    // Define o enum fora da classe, mas dentro do mesmo namespace.
    public enum DenunciaStatus
    {
        Pendente, // A denúncia foi criada, mas ainda não foi revisada.
        EmAnalise, // A denúncia está em processo de investigação.
        Concluida, // A investigação foi finalizada.
        Rejeitada // A denúncia foi considerada inválida ou duplicada.
    }
    public class DenunciaMODEL
    {
        [Key]
        public int Id_denuncia { get; set; }
        [Required]
        public string titulo_denuncia { get; set; }
        [Required]
        public string descricao_denuncia { get; set; }
        [Required]
        public DateTime dataCriacao_denuncia { get; set; }

        // Use o enum para o status da denúncia.
        [Required]
        public DenunciaStatus status_denuncia { get; set; }
        [ForeignKey("Aluno")]
        public int AlunoId { get; set; }
        public AlunoMODEL Aluno { get; set; }

        [ForeignKey("Escola")]
        public int EscolaId { get; set; }
        public EscolaMODEL Escola { get; set; }
    }
}
