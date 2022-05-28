using api_ja_cheguei_mae.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace api_ja_cheguei_mae.Services.RabbitMQ.Entidades
{
    public class TelemetriaCalculoModel
    {
       
        public Telemetria Usuario { get; set; }
        public List<TelemetriaAlvo> Alvos { get; set; }
        public int DistanciaMaxima { get; set; } = 5; //5 KM
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario">Model usuario</param>
        /// <param name="alvo">Modelo alvo</param>
        /// 
    

        public TelemetriaCalculoModel()
        {

        }

        public TelemetriaCalculoModel(Telemetria usuario, List<TelemetriaAlvo> alvos)
        {
            Usuario = usuario;
            Alvos = alvos;
        }

        public byte[] PegarMensagem()
        {
            var stringmessage = JsonSerializer.Serialize(this);
            return Encoding.UTF8.GetBytes(stringmessage);
        }

    }
}
