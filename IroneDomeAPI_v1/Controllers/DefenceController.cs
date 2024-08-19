using IroneDomeAPI_v1.Models;
using Microsoft.AspNetCore.Mvc;
using IroneDomeAPI_v1.Services;

namespace IroneDomeAPI_v1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DefenceController : ControllerBase
{
    
    [HttpPut("missiles")]
    public IActionResult Missiles(Defence defence)
    {
        DefenceService.MissileCount = defence.MissileCount;
        DefenceService.MissileTypes = defence.MissileTypes;
        return Ok();
    }
}