using api_ja_cheguei_mae.Atributttes;
using api_ja_cheguei_mae.Exceptions;
using api_ja_cheguei_mae.Request;
using api_ja_cheguei_mae.Request.Alvos;
using api_ja_cheguei_mae.Services;
using api_ja_cheguei_mae.Services.LoginService;
using api_ja_cheguei_mae.Services.Redis;
using Domain.Entities;
using Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_ja_cheguei_mae.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DispositivoController : Controller
    {
        private readonly IUsuarioService _usuario;
        private readonly MyDbContext _contexto;
        private readonly IRedisService _redis;

        public DispositivoController(IUsuarioService usuario, MyDbContext contexto, IRedisService redis)
        {
            _usuario = usuario;
            _contexto = contexto;
            _redis = redis;
        }

        [RequireAuth]
        [HttpGet("listar")]
        public IActionResult ObterDispositivos()
        {
            var usuario = _usuario.PegarPerfilLogado();
            var blogs = _contexto.Dispositivos.Include(v => v.Destino).Where(t => t.UsuarioId == usuario.UsuarioId).ToList();
            return Ok(blogs);
        }

        [RequireAuth]
        [HttpPost("cadastrar")]
        public IActionResult CadastrarDispositivo(CadastrarDispositivoRequest request)
        {
            var usuario = _usuario.PegarPerfilLogado();

            var dispositovos = _contexto.Dispositivos.Where(v => v.DeviceId.Equals(request.DeviceId)).FirstOrDefault();
            if (dispositovos != null)
            {
                 throw new GenericException(System.Net.HttpStatusCode.BadRequest, "Dispositivo já cadastrado.");

            }
            var destinos = _contexto.Destinos.Add(new Destino { Alvos = request.Alvos });

            _contexto.Dispositivos.Add(new Dispositivo
            { 
                Descricao = request.Descricao, 
                Nome = request.Nome, 
                DeviceId = request.DeviceId,
                UsuarioId = usuario.UsuarioId,
                Destino = destinos.Entity
            });
            _contexto.SaveChanges();
            return Ok();          
        }

        [RequireAuth]
        [HttpPost("deletar")]
        public IActionResult DeletarDispositivo(DeletarDispositivoRequest request)
        {
            var usuario = _usuario.PegarPerfilLogado();
            var dept = _contexto.Dispositivos.Where(a => a.DispositivoId == request.DispositivoId && a.UsuarioId == usuario.UsuarioId).Include(x => x.Destino).FirstOrDefault();
           
            _contexto.Dispositivos.Remove(dept);
            _contexto.SaveChanges();
            return Ok();
        }


        [RequireAuth]
        [HttpPost("alvos/atualizar")]
        public async Task<IActionResult> AtualizarAlvos(AtualizarAlvoRequest request)
        {
            var usuario = _usuario.PegarPerfilLogado();

            var dispositivo = _contexto.Dispositivos.Include(c => c.Destino).Where(a => a.DispositivoId == request.DispositivoId && a.UsuarioId == usuario.UsuarioId).FirstOrDefault(); //_contexto.Destinos.Find(request.DispositivoId);
            if (dispositivo == null)
            {
                return null;
            }
            var destino = _contexto.Destinos.Find(dispositivo.Destino.DestinoId);

          

            destino.Alvos = request.Alvos;
            await _contexto.SaveChangesAsync();

            _redis.Set<List<TelemetriaAlvo>>(dispositivo.DeviceId, dispositivo.Destino.Alvos, 1800);

            return Ok();
        }

        [RequireAuth]
        [HttpPost("Alvos/AtualizarPorDevice")]
        public async Task<IActionResult> AtualizarAlvos(AtualizarAlvoPorDeviceRequest request)
        {
            var usuario = _usuario.PegarPerfilLogado();

            var dispositivo = _contexto.Dispositivos.Include(c => c.Destino).Where(a => a.DeviceId.Equals(request.DeviceId) && a.UsuarioId == usuario.UsuarioId).FirstOrDefault(); //_contexto.Destinos.Find(request.DispositivoId);
            if (dispositivo == null)
            {
                return null;
            }
            var destino = _contexto.Destinos.Find(dispositivo.Destino.DestinoId);
            destino.Alvos = request.Alvos;
            await _contexto.SaveChangesAsync();

            _redis.Set<List<TelemetriaAlvo>>(dispositivo.DeviceId, dispositivo.Destino.Alvos, 1800);

            //if (_redis.Verificar(dispositivo.DeviceId))
            //{
            //    _redis.Apagar(dispositivo.DeviceId);
            //}

            return Ok();
        }

        [RequireAuth]
        [HttpPost("alvos/listar")]
        public IActionResult ObterAlvos(ObterAlvoRequest request)
        {
            var usuario = _usuario.PegarPerfilLogado();

            var dispositivo = _contexto.Dispositivos.Include(c => c.Destino).Where(t => t.DispositivoId == request.DispositivoId && t.UsuarioId == usuario.UsuarioId).FirstOrDefault(); //_contexto.Destinos.Find(request.DispositivoId);
            if (dispositivo == null)
            {
                return null;
            }

            var destino = _contexto.Destinos.Find(dispositivo.Destino.DestinoId);
            return Ok(destino);
        }

        [RequireAuth]
        [HttpPost("alvos/{deviceId}")]
        public IActionResult ObterAlvos(string deviceId)
        {
            var usuario = _usuario.PegarPerfilLogado();

            var dispositivo = _contexto.Dispositivos.Include(c => c.Destino).Where(t => t.DeviceId.Equals(deviceId) && t.UsuarioId == usuario.UsuarioId).FirstOrDefault(); //_contexto.Destinos.Find(request.DispositivoId);
            if (dispositivo == null)
            {
                return Ok(new Destino
                {
                    Alvos = new List<TelemetriaAlvo>(),
                    DestinoId = 0
                }) ;
            }

            var destino = _contexto.Destinos.Find(dispositivo.Destino.DestinoId);
            return Ok(destino);
        }


    }
}
