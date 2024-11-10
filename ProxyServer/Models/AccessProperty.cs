using Microsoft.EntityFrameworkCore;
using System;

namespace ProxyServer.Models;

public class AccessProperty
{
    public int AccessPropertyId { get; set; }
    public required string ServiceName { get; set; }
    public required int AccessCount { get; set; }
}
