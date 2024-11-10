namespace ProxyServer.DTO.ResponseDTO
{
    public class AccessResponseDTO
    {
        public required string ServiceName { get; set; }
        public required string ClientName { get; set; }
        public int ClientCurrentAccessCount { get; set; }
        public int ClientAccessCount { get; set; }
    }
}
