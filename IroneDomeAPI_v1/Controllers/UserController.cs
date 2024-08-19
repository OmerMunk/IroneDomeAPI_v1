using IroneDomeAPI_v1.Models;
using Microsoft.AspNetCore.Mvc;

namespace IroneDomeAPI_v1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(LoginObject loginObject)
    {
        if (loginObject.UserName == "admin" &&
            loginObject.Password == "123456")
        {
            return StatusCode(200
                ,new {token = "SHUBULU_TOKEN"}
                );
        }
        return StatusCode(StatusCodes.Status401Unauthorized,
                new {error = "invalid credentials"});
    }
}