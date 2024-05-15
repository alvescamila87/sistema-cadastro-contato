using ContatoDomain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ContatoWebApplication.Data
{

    /// <summary>
    /// Criando configuração do banco em memória
    /// </summary>
    /// <param name="options"></param>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
            public DbSet<Contato> Contato { get; set; }
    }
}
