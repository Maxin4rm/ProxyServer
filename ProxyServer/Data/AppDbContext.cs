using ProxyServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ProxyServer.Data;
public class AppDbContext : DbContext
{
    public DbSet<AccessProperty> AccessProperties { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}