

using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace api_ja_cheguei_mae.Services.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _redisCache;

        public RedisService(IDistributedCache redisDatabase)
        {
            _redisCache = redisDatabase;
        }

        public T Get<T>(string chave)
        {
            var value = _redisCache.GetString(chave);

            if(value != null)
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;

        }

        public T Set<T>(string chave, T valor, int expiracao)
        {
            var timeOut = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
                SlidingExpiration = TimeSpan.FromSeconds(expiracao)
            };

            _redisCache.SetString(chave, JsonSerializer.Serialize(valor), timeOut);
            return valor;
        }

        public bool Apagar(string chave)
        {
            _redisCache.Remove(chave);
            return default;
        }

        public T Set<T>(string chave, T valor)
        {
            _redisCache.SetString(chave, JsonSerializer.Serialize(valor));
            return valor;
        }

        public bool Verificar (string chave)
        {
           return _redisCache.Get(chave) != null; 
        }
    }
}
