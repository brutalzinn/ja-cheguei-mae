
using api_ja_cheguei_mae.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace api_ja_cheguei_mae.Controllers
{
    [ApiController]
    [Route("mensagem")]
    [Produces("application/json")]
    public class MensagemController : Controller
    {

        private readonly MyDbContext _contexto;

        public MensagemController( MyDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet("listarmensagem")]
        public IActionResult ListarMensagens()
        {
            return Ok(_contexto.Usuarios.ToList());
        }

        [HttpGet]
        public IActionResult PegarMensagem()
        {
            return null;
            ///return Ok(_mensagemService.PegarMensagem());
        }
    }
}
