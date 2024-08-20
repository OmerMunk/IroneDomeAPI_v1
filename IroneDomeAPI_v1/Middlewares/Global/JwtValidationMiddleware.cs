using System.IdentityModel.Tokens.Jwt;

namespace IroneDomeAPI_v1.Middlewares.Global;

public class JwtValidationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtValidationMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        
        // Headers {Authorization: Bearer ey37729ythkwaw4i}
        // Bearer ey37729ythkwaw4i
        // [Bearer,ey37729ythkwaw4i]
        string BearerToken = context.Request.Headers["Authorization"].FirstOrDefault();
        string Token = BearerToken.Split(" ").Last();

        if (Token != null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized - Token is missing");
            return;
        }
        
        
    }
}