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
        // token handler can create token
        var tokenHandler = new JwtSecurityTokenHandler();
        
        string secretKey = "12345678"; //TODO: remove this from code
        byte[] key = Encoding.ASCII.GetBytes(secretKey);

        // token descriptor describe HOW to create the token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // things to include in the token
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, userIP),
                }
            ),
            // expiration time of the token
            Expires = DateTime.Now.AddMinutes(1),
            // the secret key of the token
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
                )
        };
        
        // creating the token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        // converting the token to string
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

            string userIP = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            
            return StatusCode(200
                ,new {token = GenerateToken(userIP)}
                );
        }
        return StatusCode(StatusCodes.Status401Unauthorized,
                new {error = "invalid credentials"});
    }
}