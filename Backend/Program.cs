using Backend.Endpoints;
using Backend.Infrastructure;
using Backend.Repository;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

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

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapHub<ChatHub>("/chatHub");

app.MapUserEndpoints();

app.Run();
