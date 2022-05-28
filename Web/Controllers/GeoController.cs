using api_ja_cheguei_mae.Models;
using api_ja_cheguei_mae.Request;
using api_ja_cheguei_mae.Services.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace api_ja_cheguei_mae.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeoController : Controller
    {
        private readonly IRedisService _redis;
        private readonly IOptions<GoogleConfig> _googleConfig;
        private readonly IOptions<Ambiente> _ambienteConfig;

        public GeoController(IRedisService redis, IOptions<GoogleConfig> googleConfig, IOptions<Ambiente> ambienteConfig)
        {
            _redis = redis;
            _googleConfig = googleConfig;
            _ambienteConfig = ambienteConfig;
        }

        [HttpPost("geolocalizacao")]
        public async Task<IActionResult> GetCoordinatesForAddress(GoogleGeoCode geo_request)
        {

            if(_ambienteConfig.Value.PegarModo() == Ambiente.Environment.DEV)
            {

                var resultado = new LocationModel { Latitude = new Random().NextDouble(), Longitude = new Random().NextDouble(), Tipos = new List<string> { "" }, EnderecoFormatado = geo_request.Endereco };
                return Ok(resultado);
            }
          
                var targetUrl = $"https://maps.googleapis.com/maps/api/geocode/json" +
                    $"?address={Pluggify(geo_request.Endereco)}" +
                    $"&inputtype=textquery&fields=geometry" +
                    $"&key={_googleConfig.Value.GeoCodeKey}";


                using var client = new HttpClient();
                using var request = new HttpRequestMessage(HttpMethod.Get, targetUrl);
                using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                {
                    if (stream == null || stream.CanRead == false)
                        return null;

                    using var sr = new StreamReader(stream);
                    var jsonString = sr.ReadToEnd();
                    GeoCode responseObject = JsonConvert.DeserializeObject<GeoCode>(jsonString);

                    var results = responseObject.results;

                    var lat = results[0]?.geometry?.location?.lat;
                    var lng = results[0]?.geometry?.location?.lng;
                    var tipos = results[0]?.types;
                    var endereco_formatado = results[0]?.formatted_address;

                    var result = new LocationModel { Latitude = lat.GetValueOrDefault(), Longitude = lng.GetValueOrDefault(), Tipos = tipos, EnderecoFormatado = endereco_formatado };
                    return Ok(result);
                }
                else
                {
                   
                    throw new Exception($"UNKNOWN ERROR USING GEOCODING API :: {response}");
                }
         
                //remover depois
             string Pluggify(string address)
            {
                return address.ToLower().Replace(" ", "+");
            }
        
        
        }
          

           
        


    }
}
