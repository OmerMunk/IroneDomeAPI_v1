using IroneDomeAPI_v1.Enums;
using System.ComponentModel.DataAnnotations;


namespace IroneDomeAPI_v1.Models;

public class Attack
{
    public Guid? id { get; set; }
    
    [AllowedValues("Iran", "Hutim")]
    public string origin { get; set; }
    
    [Range(50, 500)]
    public int? damage { get; set; }
    
    public string type { get; set; }
    public AttackStatuses? status { get; set; }
    public DateTime? startedat { get; set; }
}