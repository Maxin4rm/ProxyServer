namespace ProxyServer.Services.Interfaces
{
    public interface IProxyAccessService
    {
        public List<string> GetWebServices();
        public Task<Dictionary<string, int>?> GetAccessProperties();
        public ValueTask<bool> SetAccessProperties(string key, int value);
    }
}
