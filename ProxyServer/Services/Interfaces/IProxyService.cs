namespace ProxyServer.Services.Interfaces
{
    public interface IProxyService
    {
        public Task<bool> GetRequestAbility(string url, string clientIp);
    }
}
