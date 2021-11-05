using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api_ja_cheguei_mae.Services
{
    public interface IMensagemService
    {
        public Task <ActionResult> PegarMensagem();
    }
}
