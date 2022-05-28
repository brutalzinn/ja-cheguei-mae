using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace api_ja_cheguei_mae.Services.LoginService
{
    public interface IUsuarioService
    {
        public Usuario PegarPerfilLogado();

    }
}
