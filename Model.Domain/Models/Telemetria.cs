namespace api_ja_cheguei_mae.Request
{
    public class Telemetria
    {
        public Telemetria(double lat, double longitude)
        {
            Lat = lat;
            Long = longitude;
        }
        public Telemetria()
        {

        }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
