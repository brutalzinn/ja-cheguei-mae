using System.Collections.Generic;
namespace Domain.Entities
{
    //implementar user identity depois. Vamos só migrar a estrutura nova
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public List<Dispositivo> Dispositivos { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
