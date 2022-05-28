using System.Collections.Generic;

namespace api_ja_cheguei_mae.Request.Alvos
{
    public class AtualizarAlvoPorDeviceRequest
    {
        public string DeviceId { get; set; }
        public List<TelemetriaAlvo> Alvos { get; set; }
    }
}
