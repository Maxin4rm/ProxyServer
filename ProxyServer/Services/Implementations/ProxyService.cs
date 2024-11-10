using Microsoft.EntityFrameworkCore;
using ProxyServer.Data;
using ProxyServer.Services.Interfaces;

namespace ProxyServer.Services.Implementations
{
    public class ProxyService : IProxyService
    {
        private readonly IRedisService _redisService;
        private readonly AppDbContext _context;

        public ProxyService(IRedisService redisService, AppDbContext context)
        {
            _redisService = redisService;
            _context = context;
        }

        public async Task<bool> GetRequestAbility(string url, string clientIp)
        {
            var key = $"{url}:{clientIp}";

            var entity = await _context.AccessProperties
                .FirstOrDefaultAsync(e => e.ServiceName == url);

            int limit = 10;
            if(entity is not null)
            {
                limit = entity.AccessCount;
            }
            
            var database = _redisService.GetDatabase();

            // Получаем текущее количество запросов
            var currentCount = Convert.ToInt32(await database.StringGetAsync(key)); 
                
            // Устанавливаем время жизни ключа на 1 час, если это первый запрос
            if (currentCount == 1)
            {
                await database.KeyExpireAsync(key, TimeSpan.FromHours(1));
            }

            // Проверяем, не превышен ли лимит
            if (currentCount > limit)
            {
                return false;
            }
            await database.StringIncrementAsync(key);
            return true;
        }

    }
}


           