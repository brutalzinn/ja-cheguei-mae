using api_ja_cheguei_mae.Request;
using Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace api_ja_cheguei_mae.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificacaoController : Controller
    { 
 
            private readonly MyDbContext _contexto;

            public NotificacaoController(MyDbContext contexto)
            {
                _contexto = contexto;
            }

            [HttpPost]
            public IActionResult AlvoEncontrado(TelemetriaAlvo request)
            {
                Debug.WriteLine($"Você chegou em {request.Nome}");
                return Ok();
            }
 

}
}
