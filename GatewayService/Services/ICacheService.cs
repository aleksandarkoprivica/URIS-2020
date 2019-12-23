using System.Threading.Tasks;

namespace GatewayService.Services
{
    public interface ICacheService
    {
        void SetValue(string key, string value);

        string GetValue(string key);
    }
}