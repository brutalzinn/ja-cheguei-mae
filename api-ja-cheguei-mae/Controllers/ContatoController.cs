using api_ja_cheguei_mae.Atributttes;
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

        private readonly MyDbContext _contexto;

        public ContatoController(IMensagemService mensagemService, MyDbContext contexto)
        {
            _mensagemService = mensagemService;
            _contexto = contexto;
        }
        [RequireAuth]
        [HttpGet]
        public IActionResult ListarContato()
        {
            return Ok(_contexto.Contatos.Where(v=> v.Status));
        }
    }
}
