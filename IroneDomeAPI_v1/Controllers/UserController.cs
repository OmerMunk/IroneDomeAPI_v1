using IroneDomeAPI_v1.Models;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


using System.IdentityModel.Tokens.Jwt;


namespace IroneDomeAPI_v1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private string GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        return "";
    }
    
    [HttpPost("login")]
    public IActionResult Login(LoginObject loginObject)
    {
        if (loginObject.UserName == "admin" &&
            loginObject.Password == "123456")
        {
            
            
            return StatusCode(200
                ,new {token = GenerateToken()}
                );
        }
        return StatusCode(StatusCodes.Status401Unauthorized,
                new {error = "invalid credentials"});
    }
}