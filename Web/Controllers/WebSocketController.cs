using api_ja_cheguei_mae.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using api_ja_cheguei_mae.Services.RabbitMQ.Entidades;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using api_ja_cheguei_mae.Services.Redis;
using Infra.Data.Context;

namespace api_ja_cheguei_mae.Controllers
{
    [ApiController]

    [Route("websocket")]
    public class WebSocketController : ControllerBase
    {
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "TELEMETRIA";
        private readonly MyDbContext _contexto;
        private readonly IRedisService _redis;

        public WebSocketController(MyDbContext contexto, IRedisService redis)
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            _contexto = contexto;
            _redis = redis;
        }

        [HttpGet("gps")]
        public async Task Gps()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {

                using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }


        private async Task Echo(HttpContext httpContext, WebSocket webSocket)
        {

            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
#if DEBUG
                Stopwatch timer = new Stopwatch();
                timer.Start();
#endif
                using MemoryStream ms = new MemoryStream(buffer, 0, result.Count);
                var mobileTelemetria = JsonSerializer.Deserialize<MobileTelemetria>(ms.ToArray());
                //var target = new TelemetriaAlvo(-22.973470877001226, -43.371147111206184, "TARGET");
                List<TelemetriaAlvo> alvos = new List<TelemetriaAlvo>();
                Debug.WriteLine($"DeviceId:{mobileTelemetria.DeviceId} Latitude:{mobileTelemetria.Lat} Longitude:{mobileTelemetria.Long}");

                //List<TelemetriaAlvo> alvos = new List<TelemetriaAlvo>();
                //alvos.Add(target);

                //var dispositivo = _contexto.Dispositivos.ToList();

                if (!_redis.Verificar(mobileTelemetria.DeviceId))
                {
                    var dispositivo = await _contexto.Dispositivos.Include(c => c.Destino).Where((v) => v.DeviceId.Equals(mobileTelemetria.DeviceId)).FirstAsync();
                    _redis.Set<List<TelemetriaAlvo>>(mobileTelemetria.DeviceId, dispositivo.Destino.Alvos, 1800);
                    Debug.WriteLine($"Cache adicionado em {dispositivo.DeviceId}");
                }
                alvos = _redis.Get<List<TelemetriaAlvo>>(mobileTelemetria.DeviceId);
                

                //var dispositivo = _contexto.Dispositivos.Include(c => c.destinos_id).Where((v) => v.device_uuid == mobileTelemetria.DeviceId).FirstOrDefault();


                var TelemetriaRabbitMQ = new TelemetriaCalculoModel(new Telemetria(mobileTelemetria.Lat,mobileTelemetria.Long), alvos);
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                
                using (var connection = _factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                          queue: QUEUE_NAME,
                          durable: false,
                          exclusive: false,
                          autoDelete: false,
                          arguments: null);
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: QUEUE_NAME,
                            basicProperties: null,
                            body: TelemetriaRabbitMQ.PegarMensagem());

                            
                    }
                }
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

#if DEBUG
                timer.Stop();
                Debug.WriteLine("Time Taken: " + timer.Elapsed.TotalMilliseconds.ToString("#,##0.00 'milliseconds'"));
#endif
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

        }

    }
}
