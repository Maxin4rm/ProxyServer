using Microsoft.AspNetCore.Mvc;
using ProxyServer.Services.Interfaces;
using System.Net.Http;

namespace ProxyServer.Controllers;

[Route("arcservertest/rest/services")]
[ApiController]
public class ProxyController : ControllerBase
{
    private const string servicesBaseAddr = "https://portaltest.gismap.by/arcservertest/rest/services/";
    private readonly IRedisService _redisService;
    private readonly IProxyService _proxyService;
    private readonly IProxyAccessService _proxyAccessService;
    private readonly IHttpClientFactory _httpClientFactory;

    public ProxyController(IRedisService redisService, IProxyService proxyService, IProxyAccessService proxyAccessService, IHttpClientFactory httpClientFactory)
    {
        _redisService = redisService;
        _proxyService = proxyService;
        _proxyAccessService = proxyAccessService;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("{*url}")]
    public async Task<IActionResult> Get(string url)
    {
        var clientIp = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        string serviceName = "";
        var services = _proxyAccessService.GetWebServices();
        foreach (var service in services)
        {
            if (url.Contains(service))
            {
                serviceName = service;
                break;
            }
        }
            
        url += $"{HttpContext.Request.QueryString}";
        
        if (!(await _proxyService.GetRequestAbility(serviceName, clientIp)))
        {
            return StatusCode(429, "Слишком много запросов. Попробуйте позже.");
        }

        // Перенаправляем запрос
        string targetUrl = servicesBaseAddr + url;
        var httpClient = _httpClientFactory.CreateClient();
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, targetUrl);
        var response = await httpClient.SendAsync(requestMessage);

        // Копируем статус код и заголовки из ответа
        Response.StatusCode = (int)response.StatusCode;
        foreach (var header in response.Headers)
        {
            Response.Headers[header.Key] = header.Value.ToArray();
        }

        // Если есть содержимое в ответе, возвращаем его
        var content = await response.Content.ReadAsByteArrayAsync();
        return new FileContentResult(content, response.Content.Headers!.ContentType!.ToString());
    }
}
