using Microsoft.EntityFrameworkCore;
using ProxyServer.Data;
using ProxyServer.DTO.ResponseDTO;
using ProxyServer.Services.Interfaces;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace ProxyServer.Services.Implementations;

public class RedisService : IRedisService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly AppDbContext _context;
    private readonly IProxyAccessService _proxyAccessService;

    public RedisService(IConfiguration configuration, AppDbContext context, IProxyAccessService proxyAccessService)
    {
        _context = context;
        _proxyAccessService = proxyAccessService;
        var redisConfiguration = configuration.GetSection("Redis:Configuration").Value!;
        _redis = ConnectionMultiplexer.Connect(redisConfiguration);
    }

    public IDatabase GetDatabase()
    {
        return _redis.GetDatabase();
    }

    public async Task<List<AccessResponseDTO>> GetAccessData()
    {
        var database = _redis.GetDatabase();
        var server = _redis.GetServer("redis:6379");

        // Получаем все ключи из Redis
        var redisKeys = server.Keys().ToArray();
        var data = await _proxyAccessService.GetAccessProperties();

        var response = new ConcurrentBag<AccessResponseDTO>();

        var tasks = redisKeys.Select(async key =>
        {
            var value = await database.StringGetAsync(key);
            var kvPair = GetKeyValuePairsFromServerKeys(key!);

            if (data!.TryGetValue(kvPair.Item1, out int dataValue))
            {
                var responseItem = new AccessResponseDTO()
                {
                    ClientName = kvPair.Item2,
                    ServiceName = kvPair.Item1,
                    ClientAccessCount = Convert.ToInt32(value),
                    ClientCurrentAccessCount = dataValue
                };
                response.Add(responseItem);
            }
        });

        await Task.WhenAll(tasks);

        return response.ToList();
    }

    private static (string, string) GetKeyValuePairsFromServerKeys(string key)
    {
        var index = key.IndexOf(':');
        var keyPart = key.Substring(0, index); // Подстрока до первого двоеточия
        var valuePart = key.Substring(index + 1); // Подстрока после первого двоеточия
        return (keyPart, valuePart); 
    }
}
