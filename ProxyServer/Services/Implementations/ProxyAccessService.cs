using Microsoft.EntityFrameworkCore;
using ProxyServer.Data;
using ProxyServer.Models;
using ProxyServer.Services.Interfaces;

namespace ProxyServer.Services.Implementations;

public class ProxyAccessService : IProxyAccessService
{
    private readonly List<string> webServices = ["A05_EGRNI_WGS84", "A01_ZIS_WGS84", "A06_ATE_TE_WGS84", "C01_Belarus_WGS84"];
    private readonly AppDbContext _context;

    public ProxyAccessService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, int>?> GetAccessProperties()
    {
        var result = await _context.AccessProperties
           .Select(e => new { e.ServiceName, e.AccessCount })
           .ToListAsync();

        return result.ToDictionary(e => e.ServiceName, e => e.AccessCount);
    }

    public List<string> GetWebServices()
    {
        return webServices;
    }

    public async ValueTask<bool> SetAccessProperties(string key, int value)
    {
        var accessProperty = new AccessProperty() 
        { 
            ServiceName = key,
            AccessCount = value,
        };

        var entity = await _context.AccessProperties
                .FirstOrDefaultAsync(e => e.ServiceName == key);

        if (entity != null)
        {
            // Обновляем AccessCount
            entity.AccessCount = value;

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            // Обработка случая, когда запись не найдена (опционально)
            return false;
        }
    }

}
