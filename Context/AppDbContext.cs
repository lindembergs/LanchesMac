using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Context;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Lanche> Lanches { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lanche>()
            .Property(l => l.Preco)
            .HasPrecision(10, 2);

        base.OnModelCreating(modelBuilder);
    }
}
