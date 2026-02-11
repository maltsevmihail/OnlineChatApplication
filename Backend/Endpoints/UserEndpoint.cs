using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;
using Backend.Models;
using Backend.Services;

namespace Backend.Endpoints
{
    public static class UserEndpoint
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);

            return app;
        }
        public static async Task<IResult> Register(RegisterUserRequest request , UserService userService)
        {
            await userService.Register(request.login, request.password);

            return Results.Ok();
        }
        public static async Task<IResult> Login()
        {
            return null;
        }
    }
}