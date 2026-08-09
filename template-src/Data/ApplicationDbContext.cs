using Desafio.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Data;
/// <summary>
/// Arquivo de acesso ao banco de dados da aplicação e configura o mapeamento das entidades usando Entity Framework Core.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>()
            .Property(produto => produto.Preco)
            .HasPrecision(18, 2);
    }
}
