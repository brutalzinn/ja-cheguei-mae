using api_ja_cheguei_mae.PostgreeSQL;
using api_ja_cheguei_mae.Services.JWTService;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace api_ja_cheguei_mae.Services.LoginService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DatabaseContexto _contexto;
        private readonly IJWTService _jwtService;

        public UsuarioService(DatabaseContexto contexto, IJWTService jwtService)
        {
            _contexto = contexto;
            _jwtService = jwtService;
        }

        public UsuarioModel PegarPerfilLogado()
        {
            var usuario = _contexto.usuario.Where((v) => v.id == _jwtService.Id).FirstOrDefault();
            return usuario;
        }
    }
}
