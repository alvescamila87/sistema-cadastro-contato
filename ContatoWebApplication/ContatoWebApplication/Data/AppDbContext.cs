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
            public DbSet<Email> Email { get; set; }

        // Configuração chave primária para Contato e E-mail

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurando a chave primária para a classe Contato
            modelBuilder.Entity<Contato>().HasKey(contato => contato.id);

            // Configurando a chave primária para a classe Email
            modelBuilder.Entity<Email>().HasKey(emailContato => emailContato.id);
        }
    }
}
