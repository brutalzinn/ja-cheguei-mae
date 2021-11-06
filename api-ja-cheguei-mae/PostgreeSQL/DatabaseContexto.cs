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
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<MensagemModel> Mensagem { get; set; }
        public DbSet<ContatoModel> Contato { get; set; }

    }

    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }

    public class ContatoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public UsuarioModel Usuario_Id { get; set; }
        public string Numero { get; set; }
    }
    public class MensagemModel
    {
        public int Id { get; set; }
        public ContatoModel Contato_Id { get; set; }
        public bool Status { get; set; }
        public string Mensagem { get; set; }
    }



}
