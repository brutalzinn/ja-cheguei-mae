using System.Collections.Generic;

namespace api_ja_cheguei_mae.Request.Alvos
{
    public class AtualizarAlvoRequest
    {
        public int DispositivoId { get; set; }
        public List<TelemetriaAlvo> Alvos { get; set; }

    }
}
