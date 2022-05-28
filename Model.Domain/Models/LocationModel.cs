using System.Collections.Generic;

namespace api_ja_cheguei_mae.Models
{
    public class LocationModel
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? EnderecoFormatado { get; set; }
        public IEnumerable<string>? Tipos { get; set; }
    }
}
