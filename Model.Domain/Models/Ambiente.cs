namespace api_ja_cheguei_mae.Models
{
    public class Ambiente
    {
        public string Modo { get; set; }

       public enum Environment
        {
            DEV,
            PROD,
            TEST
        }

        public Environment PegarModo()
        {
            switch (this.Modo)
            {
                case "DEV":
                    return Environment.DEV;
                case "PROD":
                    return Environment.PROD;
                default:
                    return Environment.TEST;
            }
        }
    }
}
