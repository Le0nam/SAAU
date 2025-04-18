using Microsoft.EntityFrameworkCore;
using SAAU.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    public DbSet<Coordenador> Coordenadores { get; set; }
    public DbSet<Atendimento> Atendimentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Atendimento>(entity =>
        {
            entity.Property(a => a.HoraInicio).HasColumnType("time");
            entity.Property(a => a.HoraFim).HasColumnType("time");

            entity.HasOne(a => a.Coordenador)
                  .WithMany(c => c.Atendimentos)
                  .HasForeignKey(a => a.CoordenadorId);
        });

        modelBuilder.Entity<Agendamento>(entity =>
        {
            entity.HasOne(a => a.Aluno)
                  .WithMany()
                  .HasForeignKey("AlunoId")
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Coordenador)
                  .WithMany()
                  .HasForeignKey("CoordenadorId")
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
