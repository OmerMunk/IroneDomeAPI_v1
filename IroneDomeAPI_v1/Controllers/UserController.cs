using IroneDomeAPI_v1.Models;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


using System.Text;


namespace IroneDomeAPI_v1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private string GenerateToken(string userIP)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        string secretKey = "12345678"; //TODO: remove this from code
        byte[] key = Encoding.ASCII.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, userIP),
                }
            ),
            Expires = DateTime.Now.AddMinutes(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
                )
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
        
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