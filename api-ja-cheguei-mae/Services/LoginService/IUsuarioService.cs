using api_ja_cheguei_mae.PostgreeSQL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace api_ja_cheguei_mae.Services.LoginService
{
    public interface IUsuarioService
    {
        public UsuarioModel PegarPerfilLogado();

    }
}
