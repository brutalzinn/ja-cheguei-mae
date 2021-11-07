using api_ja_cheguei_mae.Services.JWTService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace api_ja_cheguei_mae.Services.LoginService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly MyDbContext _contexto;
        private readonly IJWTService _jwtService;

        public UsuarioService(MyDbContext contexto, IJWTService jwtService)
        {
            _contexto = contexto;
            _jwtService = jwtService;
        }

        public Usuario PegarPerfilLogado()
        {
            var usuario = _contexto.Usuarios.Include(c => c.Contatos).Where((v) => v.UsuarioId == _jwtService.Id).FirstOrDefault();
            return usuario;
        }
    }
}
