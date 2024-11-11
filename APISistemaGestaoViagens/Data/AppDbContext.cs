using Microsoft.EntityFrameworkCore;
using APISistemaGestaoViagens.Model.Entities;

namespace APISistemaGestaoViagens.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Destino> Destino { get; set; }
    public DbSet<Viagem> Viagem { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Pacote> Pacotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Cliente)
            .WithMany(c => c.Reservas)
            .HasForeignKey(r => r.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Viagem>()
            .HasOne(v => v.Destino)
            .WithMany()
            .HasForeignKey(v => v.DestinoId);
            
        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Cpf)
            .IsUnique();
                
        base.OnModelCreating(modelBuilder);
    }
}