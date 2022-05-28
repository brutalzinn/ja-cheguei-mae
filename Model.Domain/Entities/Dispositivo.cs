
namespace Domain.Entities
{
    public class Dispositivo
    {

        public int DispositivoId { get; set; }
        public string DeviceId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        public Destino Destino { get; set; }

    }
}
