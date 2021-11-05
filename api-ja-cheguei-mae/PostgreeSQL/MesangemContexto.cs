using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api_ja_cheguei_mae.PostgreeSQL
{
    public class DatabaseContexto : DbContext
    {
        public DbSet<Mensagem> MensagemData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=teste;Username=root;Password=root");
    }

    public class Mensagem
    {
        public int MensagemId { get; set; }

        public string ?MensagemTexto { get; set; }
    }

    
    
    
}
