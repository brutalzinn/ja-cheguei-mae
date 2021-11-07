using api_ja_cheguei_mae.Atributttes;
using api_ja_cheguei_mae.Exceptions;
using api_ja_cheguei_mae.PostgreeSQL;
using api_ja_cheguei_mae.Request;
using api_ja_cheguei_mae.Response;
using api_ja_cheguei_mae.Services;
using api_ja_cheguei_mae.Services.JWTService;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace api_ja_cheguei_mae.Controllers
{
    [ApiController]
    [Route("login")]
    [Produces("application/json")]
    public class UsuarioController : Controller
    {
        private readonly IMensagemService _mensagemService;
        private readonly IJWTService _jwtService;
        private readonly DatabaseContexto _contexto;
        public UsuarioController(IMensagemService mensagemService, IJWTService jwtService, DatabaseContexto contexto)
        {
            _mensagemService = mensagemService;
            _jwtService = jwtService;
            _contexto = contexto;
        }
        [RequireAuth]
        [HttpGet("teste")]
        public IActionResult Teste()
        {
            return Ok(_jwtService.Email);
        }
        [HttpPost]
        public IActionResult Login(LoginRequest body)
        {
            var usuario = _contexto.usuario.Where((v) => v.email == body.email && v.senha == body.senha).FirstOrDefault();
            if (usuario == null)
            {
                throw new GenericException(System.Net.HttpStatusCode.Unauthorized, "Email ou senha errados.");
            }
            var response = new LoginResponse();
            response.Token = _jwtService.GerarToken(usuario);
            response.Status = true;
            return Ok(response);
        }
    }
}
