using System.Collections.Generic;

namespace api_ja_cheguei_mae.Request.Alvos
{
    public class CadastrarDispositivoRequest
    {
        public string DeviceId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<TelemetriaAlvo> Alvos { get; set; }
    }
}
