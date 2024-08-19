using Microsoft.AspNetCore.Mvc;

namespace IroneDomeAPI_v1.utils;

[ApiController]
public static class HttpUtils
{
    public static object Response(int status, object message)
    {
        bool success = status >= 200 && status < 300;
        return new
        {
            success = success,
            message = message
        };
    }
}