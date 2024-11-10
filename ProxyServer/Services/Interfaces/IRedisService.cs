using ProxyServer.DTO.ResponseDTO;
using StackExchange.Redis;

namespace ProxyServer.Services.Interfaces
{
    public interface IRedisService
    {
        public IDatabase GetDatabase();
        public Task<List<AccessResponseDTO>> GetAccessData();
    }
}
