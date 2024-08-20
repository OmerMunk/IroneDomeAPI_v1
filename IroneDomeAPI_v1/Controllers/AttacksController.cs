using IroneDomeAPI_v1.Enums;
using Microsoft.AspNetCore.Mvc;
using IroneDomeAPI_v1.Models;
using IroneDomeAPI_v1.Services;
using IroneDomeAPI_v1.utils;
using IroneDomeAPI_v1.Middlewares;
using IroneDomeAPI_v1.Middlewares.Attack;
using Microsoft.EntityFrameworkCore;

namespace IroneDomeAPI_v1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttacksController : ControllerBase
{
    
    private readonly IronDomeContext _context;
    private readonly ILogger<AttacksController> _logger;
    
    public AttacksController(ILogger<AttacksController> logger, IronDomeContext context)
    {
        
        this._context = context;
        this._logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAttacks()
    {
        int status = StatusCodes.Status200OK;
        var attacks = await this._context.attacks.ToListAsync();
        return StatusCode(
            status,
            HttpUtils.Response(status, new {attacks = attacks})
            );
    }


    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAttack(Attack attack)
    {
        attack.status = AttackStatuses.PENDING;
        // this._context.attacks.Add(attack);
        this._context.attacks.Add(attack);
        await this._context.SaveChangesAsync();
        return StatusCode(
            StatusCodes.Status201Created,
            new {success = true, attack = attack}
            );
    }

    [HttpPost("{id}/start")]
    public async Task<IActionResult> StartAttack(Guid id)
    {
        Attack attack = await this._context.attacks.FindAsync(id);
        int status = StatusCodes.Status404NotFound;
        if (attack == null) return StatusCode(status, HttpUtils.Response(status, "Attack not found"));
        if (attack.status == AttackStatuses.COMPLETED)
        {
            status = StatusCodes.Status400BadRequest;
            return StatusCode(
                status,
                new {
                    success = false,
                    error = "Cannot start an attack that hasalready beencompleted." 
                }
            );
        }
        attack.startedat = DateTime.Now;
        attack.status = AttackStatuses.IN_PROGRESS;
        Task attackTask = Task.Run(() =>
        {
            Task.Delay(10000);
        });
        return StatusCode(
            StatusCodes.Status200OK,
            new { message = "Attack Started.", TaskId = attackTask.Id }
        );

    }


    [HttpGet("{id}/status")]
    public IActionResult AttackStatus(Guid id)
    {
        int status;
        Attack attack = this._context.attacks.FirstOrDefault(att => att.id == id);

        if (attack == null)
        {
            status = StatusCodes.Status404NotFound;
            return StatusCode(status, HttpUtils.Response(status, "attack not found"));
        }

        status = StatusCodes.Status200OK;
        return StatusCode(status, HttpUtils.Response(status, new { attack = attack }));
    }
}