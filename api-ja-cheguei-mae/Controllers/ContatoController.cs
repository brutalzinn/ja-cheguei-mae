using api_ja_cheguei_mae.Atributttes;
using api_ja_cheguei_mae.PostgreeSQL;
using api_ja_cheguei_mae.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace api_ja_cheguei_mae.Controllers
{
    [ApiController]
    [Route("contato")]
    [Produces("application/json")]
    public class ContatoController : Controller
    {
        private readonly IMensagemService _mensagemService;

        private readonly DatabaseContexto _contexto;

        public ContatoController(IMensagemService mensagemService, DatabaseContexto contexto)
        {
            _mensagemService = mensagemService;
            _contexto = contexto;
        }
        [RequireAuth]
        [HttpGet]
        public IActionResult ListarContato()
        {
            return Ok(_contexto.contato.Where(v=> v.status && v.usuario_id == 1));
        }
    }
}
