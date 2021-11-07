using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api_ja_cheguei_mae
{
    public class MyDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        public DbSet<Mensagem> Mensagens { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options)
            :base(options)
        {
         //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchoolDBContext, EF6Console.Migrations.Configuration>());

        }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany(b => b.Contatos)
                .WithOne();

            modelBuilder.Entity<Contato>()
               .HasMany(b => b.Mensagens)
               .WithOne();
        }

    }


    public class Usuario
    {
        public int UsuarioId { get; set; }
        public List<Contato> Contatos { get; set; }

        public string Email { get; set; }
        public string Senha { get; set; }

    }
    public class Contato
    {
        public int ContatoId { get; set; }

        public string Nome { get; set; }
        public bool Status { get; set; }

        public string Numero { get; set; }

        public List<Mensagem> Mensagens { get; set; }

    }


    public class Mensagem
    {
        public int MensagemId { get; set; }
        public bool Status { get; set; }
        public string CorpoMensagem { get; set; }
    }



}
