using api_ja_cheguei_mae.Request;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Destino
    {
        public int DestinoId { get; set; }
        [NotMapped]
        public List<TelemetriaAlvo> Alvos { get; set; }
    }
}
