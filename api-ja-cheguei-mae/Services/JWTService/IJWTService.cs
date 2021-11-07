using System.Collections.Generic;
namespace api_ja_cheguei_mae.Services.JWTService
{
    public interface IJWTService
    {
        public string Token { get; set; } 

        public int Id { get; set; }

        public string Email { get; set; }
        public string GerarToken(Usuario usuario);
        public void ValidarJWT(string token);
    }
}
