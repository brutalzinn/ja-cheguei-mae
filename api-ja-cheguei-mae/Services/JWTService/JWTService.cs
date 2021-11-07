using api_ja_cheguei_mae.Exceptions;
using api_ja_cheguei_mae.PostgreeSQL;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;

namespace api_ja_cheguei_mae.Services.JWTService
{
    public class JWTService : IJWTService
    {
        private string _token { get; set; }
        private string _email { get; set; }
        private int _id { get; set; }
        public string Token { get => _token; set => _token = value; }
        public int Id { get => _id; set => _id = value; }
        public string Email { get => _email; set => _email = value; }

        private const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        public string GerarToken(UsuarioModel usuario)
        {
            var payload = new Dictionary<string, object>
            {
                { "UserId", (int)usuario.id },
                { "UserEmail", usuario.email }
            };
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secret);
        }

        public void ValidarJWT(string token)
        {
            try
            {
                _token = token;
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                var payload = decoder.DecodeToObject<IDictionary<string, object>>(_token, secret, verify: true);
                _id = Convert.ToInt32(payload["UserId"]);
                _email = (string)payload["UserEmail"];
            }
            catch (TokenExpiredException)
            {
                throw new GenericException(System.Net.HttpStatusCode.Unauthorized, "Token expirou.");

            }
            catch (SignatureVerificationException)
            {
                throw new GenericException(System.Net.HttpStatusCode.Unauthorized, "Token tem uma assinatura ínválida");
            }
            catch (InvalidTokenPartsException)
            {
                throw new GenericException(System.Net.HttpStatusCode.Unauthorized, "O token deve consistir em 3 partes delimitadas por pontos");

            }

        }

        public void PegarPayload(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
