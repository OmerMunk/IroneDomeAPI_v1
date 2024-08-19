using IroneDomeAPI_v1.Models;

namespace IroneDomeAPI_v1.Services;


public interface IDbService<T>
{
   public List<T> Attacks { get; set; }
}

public class DbService: IDbService<Attack>
{
   public List<Attack> Attacks { get; set; } = new List<Attack>();
}