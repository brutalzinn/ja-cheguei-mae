using api_ja_cheguei_mae.PostgreeSQL;
using api_ja_cheguei_mae.Request;
using api_ja_cheguei_mae.Response;
using api_ja_cheguei_mae.Services;
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
        private readonly DatabaseContexto _contexto;

        public UsuarioController(IMensagemService mensagemService, DatabaseContexto contexto)
        {
            _mensagemService = mensagemService;
            _contexto = contexto;
        }
        [HttpPost]
        public IActionResult Login(LoginRequest body)
        {
            bool userExiste = _contexto.usuario.Where((v) => v.email == body.email && v.senha == body.senha).Any();
            var response = new LoginResponse(userExiste);
            response.Token = "MOCKADO";
            return Ok(response);
        }
    }
}
