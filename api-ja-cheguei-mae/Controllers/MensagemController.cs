using Microsoft.AspNetCore.Mvc;

namespace api_ja_cheguei_mae.Controllers
{
    public class MensagemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
