namespace APISistemaGestaoViagens.Data;

using Microsoft.EntityFrameworkCore;
using APISistemaGestaoViagens.Model.Entities;

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
        base.OnModelCreating(modelBuilder);
    }
}