using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProxyServer.Data;
using ProxyServer.Services;
using ProxyServer.Services.Implementations;
using ProxyServer.Services.Interfaces;
using StackExchange.Redis;
using System;

namespace ProxyServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins(["http://localhost:3000", "http://localhost:3001"])
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });

        builder.Services.AddControllers();
        builder.Services.AddHttpClient();
        builder.Services.AddAuthorization();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IRedisService, RedisService>();
        builder.Services.AddScoped<IProxyAccessService, ProxyAccessService>();
        builder.Services.AddScoped<IProxyService, ProxyService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowSpecificOrigin");
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
