using IroneDomeAPI_v1.Enums;
using System.ComponentModel.DataAnnotations;


namespace IroneDomeAPI_v1.Models;

public class Attack
{
    public Guid? Id { get; set; }
    
    [AllowedValues("Iran", "Hutim")]
    public string Origin { get; set; }
    
    [Range(50, 500)]
    public int? Damage { get; set; }
    
    public string Type { get; set; }
    public AttackStatuses? Status { get; set; }
    public DateTime? StartedAt { get; set; }
}