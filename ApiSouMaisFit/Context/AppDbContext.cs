using ApiSouMaisFit.Models;
using Microsoft.EntityFrameworkCore;
namespace ApiSouMaisFit.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Aluno> Alunos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>().HasData(
             new Aluno
             {
                 Id = 1,
                 Nome = "João",
                 Email = "joao@gmail.com",
                 Idade = 17
             },
             new Aluno
             {
                 Id = 2,
                 Nome = "Maria",
                 Email = "maria@gmail.com",
                 Idade = 18
             }
            );
    }
}
