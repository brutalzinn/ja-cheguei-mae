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
        private readonly IMensagemService _mensagemService;

        private readonly MyDbContext _contexto;

        public MensagemController(IMensagemService mensagemService, MyDbContext contexto)
        {
            _mensagemService = mensagemService;
            _contexto = contexto;
        }

        [HttpGet("/listarmensagem")]
        public IActionResult ListarMensagens()
        {
            return Ok(_contexto.Usuarios.ToList());
        }
        [HttpGet("/mensagem")]

        public IActionResult PegarMensagem()
        {
            return Ok(_mensagemService.PegarMensagem());
        }
    }
}
