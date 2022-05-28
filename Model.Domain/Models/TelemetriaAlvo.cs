using System;

namespace api_ja_cheguei_mae.Request
{
    public class TelemetriaAlvo
    {
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Nome { get; set; }
        public double DistanciaMax { get; set; }

        private string AlvoId;

        //bizarrro!
        public string Id
        {
            get
            {
                if (string.IsNullOrEmpty(AlvoId))
                {
                    AlvoId = Guid.NewGuid().ToString();
                }
                return AlvoId;
            }
            set
            {
            
                    AlvoId = Guid.NewGuid().ToString();
            }
        }
        public TelemetriaAlvo()
        {

        }

        public TelemetriaAlvo(double lat, double Longitude, string nome = "DEFAULT", double distanciaMax = 5)
        {
            Lat = lat;
            Long = Longitude;
            Nome = nome;
            DistanciaMax = distanciaMax;
        }
    }
}
