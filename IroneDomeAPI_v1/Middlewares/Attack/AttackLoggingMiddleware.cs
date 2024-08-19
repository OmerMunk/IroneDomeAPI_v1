
using Microsoft.AspNetCore.Mvc;

namespace IroneDomeAPI_v1.Middlewares.Attack;

public class AttackLoggingMiddleware
{
    
    private readonly RequestDelegate _next;

    public AttackLoggingMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        Console.WriteLine($"Inside AttackLoggingMiddleware");
        await this._next(context);
    }
}


