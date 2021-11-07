using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api_ja_cheguei_mae.PostgreeSQL
{
    public class DatabaseContexto : DbContext
    {
        
        public DatabaseContexto(DbContextOptions<DatabaseContexto> options)
            :base(options)
        {
        }
        public DbSet<UsuarioModel> usuario { get; set; }
        public DbSet<MensagemModel> mensagem { get; set; }
        public DbSet<ContatoModel> contato { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContatoModel>()
                .HasOne(p => p.Usuario)
                .WithMany(b => b.Contatos)
                .HasForeignKey(p => p.usuario_id);
        }

    }

    public class UsuarioModel
    {
        public int id { get; set; }

        public string email { get; set; }
        public string senha { get; set; }

        public ICollection<ContatoModel> Contatos { get; set; } = new List<ContatoModel>();


    }

    public class ContatoModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public bool status { get; set; }

        public UsuarioModel Usuario { get; set; }
        public int usuario_id { get; set; }
        public string numero { get; set; }
    }
    public class MensagemModel
    {
        public int id { get; set; }
        public int contato_id { get; set; }
        public bool status { get; set; }
        public string mensagem { get; set; }
    }



}
