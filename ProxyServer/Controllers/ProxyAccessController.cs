using Microsoft.AspNetCore.Mvc;
using ProxyServer.Services.Interfaces;

namespace ProxyServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProxyAccessController : ControllerBase
{
    private readonly IRedisService _redisService;
    private readonly IProxyAccessService _proxyAccessService;

    public ProxyAccessController(IRedisService redisService, IProxyAccessService proxyAccessService)
    {
        _redisService = redisService;
        _proxyAccessService = proxyAccessService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRedisData()
    {
        var result = await _redisService.GetAccessData();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SetAccessProperties(string key, int value)
    {
        var result = await _proxyAccessService.SetAccessProperties(key, value);
        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

}
