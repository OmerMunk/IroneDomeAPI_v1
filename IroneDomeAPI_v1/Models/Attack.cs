using IroneDomeAPI_v1.Enums;

namespace IroneDomeAPI_v1.Models;

public class Attack
{
    public Guid? Id { get; set; }
    public string Origin { get; set; }
    public string Type { get; set; }
    public AttackStatuses? Status { get; set; }
    public DateTime? StartedAt { get; set; }
}