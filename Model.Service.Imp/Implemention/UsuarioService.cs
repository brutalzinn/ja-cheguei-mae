using api_ja_cheguei_mae.Services.JWTService;
using Domain.Entities;
using Infra.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace api_ja_cheguei_mae.Services.LoginService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly MyDbContext _contexto;
        private readonly IJWTService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioService(MyDbContext contexto, IJWTService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _contexto = contexto;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public Usuario PegarPerfilLogado()
        {
            int userId = (int)_httpContextAccessor.HttpContext.Items["UserId"];
            var usuario = _contexto.Usuarios.Include(c => c.Dispositivos).Where((v) => v.UsuarioId == userId).FirstOrDefault();
            return usuario;
        }
    }
}
