using Backend.Endpoints;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
   var connection = builder.Configuration.GetConnectionString("redis");
   options.Configuration = connection; 
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddDbContext<UserRepository>(options =>
{
   options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), 
                    new MySqlServerVersion(new Version(8, 0, 40)));
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors();

app.MapHub<ChatHub>("/chatHub");

app.MapUserEndpoints();

app.Run();
