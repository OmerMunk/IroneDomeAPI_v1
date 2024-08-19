using IroneDomeAPI_v1.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IroneDomeAPI_v1.Models;
using IroneDomeAPI_v1.Services;
using IroneDomeAPI_v1.utils;


namespace IroneDomeAPI_v1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AttacksController : ControllerBase
{

    [HttpGet]
    public IActionResult GetAttacks()
    {
        int status = StatusCodes.Status200OK;
        return StatusCode(
            status,
            HttpUtils.Response(status, new {attacks = DbService.AttacksList.ToArray()})
            );
    }


    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateAttack(Attack attack)
    {
        attack.Id = Guid.NewGuid();
        attack.Status = AttackStatuses.PENDING;
        DbService.AttacksList.Add(attack);
        return StatusCode(
            StatusCodes.Status201Created,
            new {success = true, attack = attack}
            );
    }

    [HttpPost("{id}/start")]
    public IActionResult StartAttack(Guid id)
    {
        Attack attack = DbService.AttacksList.FirstOrDefault(att => att.Id == id);
        int status = StatusCodes.Status404NotFound;
        if (attack == null) return StatusCode(status, HttpUtils.Response(status, "Attack not found"));
        if (attack.Status == AttackStatuses.COMPLETED)
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
        attack.StartedAt = DateTime.Now;
        attack.Status = AttackStatuses.IN_PROGRESS;
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
        Attack attack = DbService.AttacksList.FirstOrDefault(att => att.Id == id);

        if (attack == null)
        {
            status = StatusCodes.Status404NotFound;
            return StatusCode(status, HttpUtils.Response(status, "attack not found"));
        }

        status = StatusCodes.Status200OK;
        return StatusCode(status, HttpUtils.Response(status, new { attack = attack }));
    }
}