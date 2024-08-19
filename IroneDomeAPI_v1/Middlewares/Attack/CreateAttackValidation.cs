namespace IroneDomeAPI_v1.Middlewares.Attack;

using System.Text.Json;

public class CreateAttackValidation
{
    private readonly RequestDelegate _next;

    public CreateAttackValidation(RequestDelegate next)
    {
        _next = next;
    }


    public async Task Invoke(HttpContext httpContext)
    {
        var request = httpContext.Request;
        string body = GetRequestBodyAsync(request.Body);
        if (!string.IsNullOrEmpty(body))
        {
            var document = JsonDocument.Parse(body);
            if (!document.RootElement.TryGetProperty("origin"))
        }


    }


    private string GetRequestBodyAsync(object body)
    {
        return "";
    }
    
}