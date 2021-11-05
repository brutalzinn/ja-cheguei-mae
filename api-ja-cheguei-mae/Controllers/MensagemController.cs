using api_ja_cheguei_mae.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_ja_cheguei_mae.Controllers
{
    [ApiController]
    [Route("mensagem")]
    [Produces("application/json")]
    public class MensagemController : Controller
    {
        private readonly IMensagemService _mensagemService;

        public MensagemController(IMensagemService mensagemService)
        {
            _mensagemService = mensagemService;
        }

        [HttpGet("/mensagem")]
        public IActionResult PegarMensagem()
        {
            return Ok(_mensagemService.PegarMensagem());
        }
    }
}
