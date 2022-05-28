using System.Threading.Tasks;

namespace api_ja_cheguei_mae.Services.Redis
{
    public interface IRedisService
    {

        T Get<T>(string chave);
        T Set<T>(string chave, T valor, int expiracao);
        T Set<T>(string chave, T valor);
        bool Apagar (string chave);
        bool Verificar(string chave);

    }
}
