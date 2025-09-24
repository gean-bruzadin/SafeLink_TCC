using Microsoft.EntityFrameworkCore;

namespace SafeLink_TCC.Config
{
    public class DbConfig : DbContext
    {
        public DbConfig(DbContextOptions<DbConfig> options) : base(options) { }

        public DbSet<Models.AlunoMODEL> Alunos { get; set; }
        public DbSet<Models.AnexoDenunciaMODEL> AnexosDenuncia { get; set; }
        public DbSet<Models.EstadoMODEL> Estados { get; set; }
        public DbSet<Models.FuncionarioMODEL> Funcionarios { get; set; }
        public DbSet<Models.Testemunha_DenunciaMODEL> TestemunhasDenuncia { get; set; }

        // novos
        public DbSet<Models.UsuarioMODEL> Usuarios { get; set; }
        public DbSet<Models.NivelMODEL> Niveis { get; set; }
        public DbSet<Models.CidadeMODEL> Cidade { get; set; }
        public DbSet<Models.EscolaMODEL> Escola { get; set; }
        public DbSet<Models.CargoMODEL> Cargo { get; set; }
        public DbSet<Models.DenunciaMODEL> Denuncia { get; set; }
        public DbSet<Models.Denuncia_TestemunhaMODEL> Denuncia_Testemunha { get; set; }
        public DbSet<Models.DenunciaAnexoMODEL> Denuncia_Anexo { get; set; } 

    }
}
