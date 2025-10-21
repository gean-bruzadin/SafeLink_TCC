using Microsoft.EntityFrameworkCore;
using SafeLink_TCC.Controllers;
using SafeLink_TCC.Models;

namespace SafeLink_TCC.Config
{
    public class DbConfig : DbContext
    {
        public DbConfig(DbContextOptions<DbConfig> options) : base(options) { }

        // DbSets de todas as tabelas
        public DbSet<EstadoMODEL> Estados { get; set; }
        public DbSet<CidadeMODEL> Cidades { get; set; }
        public DbSet<CargoMODEL> Cargos { get; set; }
        public DbSet<UsuarioMODEL> Usuarios { get; set; }
        public DbSet<FuncionarioMODEL> Funcionarios { get; set; }
        public DbSet<EscolaMODEL> Escolas { get; set; }
        public DbSet<AlunoMODEL> Alunos { get; set; }
        public DbSet<TestemunhaMODEL> Testemunhas { get; set; }
        public DbSet<DenunciaMODEL> Denuncias { get; set; }
        public DbSet<AnexoMODEL> Anexos { get; set; }
        public DbSet<RespostaMODEL> Respostas { get; set; }
        public DbSet<Denuncia_TestemunhaMODEL> DenunciaTestemunhas { get; set; }
        public DbSet<NivelMODEL> Niveis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Relacionamento N:N Denuncia ↔ Testemunha
            modelBuilder.Entity<Denuncia_TestemunhaMODEL>()
                .HasKey(dt => new { dt.Id_Denuncia, dt.Id_Testemunha });

            modelBuilder.Entity<Denuncia_TestemunhaMODEL>()
                .HasOne(dt => dt.Denuncias)
                .WithMany(d => d.DenunciaTestemunhas)
                .HasForeignKey(dt => dt.Id_Denuncia);

            modelBuilder.Entity<Denuncia_TestemunhaMODEL>()
                .HasOne(dt => dt.Testemunha)
                .WithMany(t => t.DenunciaTestemunhas)
                .HasForeignKey(dt => dt.Id_Testemunha);

            // 🔹 Estado -> Cidade (1:N)
            modelBuilder.Entity<CidadeMODEL>()
                .HasOne(c => c.Estado)
                .WithMany(e => e.Cidades)
                .HasForeignKey(c => c.Id_Estado);

            // 🔹 Cidade -> Escola (1:N)
            modelBuilder.Entity<EscolaMODEL>()
                .HasOne(e => e.Cidade)
                .WithMany(c => c.Escolas)
                .HasForeignKey(e => e.Id_Cidade);

            // 🔹 Cargo -> Funcionarios (1:N)
            modelBuilder.Entity<FuncionarioMODEL>()
                .HasOne(f => f.Cargo)
                .WithMany(c => c.Funcionarios)
                .HasForeignKey(f => f.Id_Cargo);

            // 🔹 Escola -> Denuncia (1:N)
            modelBuilder.Entity<DenunciaMODEL>()
                .HasOne(d => d.Escola)
                .WithMany(e => e.Denuncias)
                .HasForeignKey(d => d.Id_Escola);

            // 🔹 Aluno -> Denuncia (1:N)
            modelBuilder.Entity<DenunciaMODEL>()
                .HasOne(d => d.Aluno)
                .WithMany(a => a.Denuncias)
                .HasForeignKey(d => d.Id_Aluno);

            // 🔹 Denuncia -> Anexos (1:N)
            modelBuilder.Entity<AnexoMODEL>()
                .HasOne(a => a.Denuncia)
                .WithMany(d => d.Anexos)
                .HasForeignKey(a => a.Id_Denuncia);

            // 🔹 Denuncia -> Respostas (1:N)
            modelBuilder.Entity<RespostaMODEL>()
                .HasOne(r => r.Denuncia)
                .WithMany(d => d.Respostas)
                .HasForeignKey(r => r.Id_Denuncia);


        }
    }
}
