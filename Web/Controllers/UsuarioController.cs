using api_ja_cheguei_mae.Atributttes;
using api_ja_cheguei_mae.Exceptions;
using api_ja_cheguei_mae.Request;
using api_ja_cheguei_mae.Response;
using api_ja_cheguei_mae.Services;
using api_ja_cheguei_mae.Services.JWTService;
using api_ja_cheguei_mae.Services.LoginService;
using Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace api_ja_cheguei_mae.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJWTService _jwtService;
        private readonly MyDbContext _contexto;

        public UsuarioController(IUsuarioService usuarioService, IJWTService jwtService, MyDbContext contexto)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
            _contexto = contexto;
        }

        [RequireAuth]
        [HttpGet("teste")]
        public IActionResult Teste()
        {
            int userId = Convert.ToInt32(HttpContext.Request.Headers["UserId"]);
            return Ok(_usuarioService.PegarPerfilLogado());
        }
        [HttpPost("login")]
        public IActionResult Login(Login body)
        {
            var usuario = _contexto.Usuarios.Where((v) => v.Email == body.email && v.Senha == body.senha).FirstOrDefault();
            if (usuario == null)
            {
                throw new GenericException(System.Net.HttpStatusCode.Unauthorized, "Email ou senha errados.");
            }
            var response = new LoginResponse();
            response.Token = _jwtService.GerarToken(usuario);
            response.Status = true;
            response.Email = usuario.Email;
            return Ok(response);
        }
    }
}
