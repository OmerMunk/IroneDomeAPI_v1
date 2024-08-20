using IroneDomeAPI_v1.Models;
using Microsoft.EntityFrameworkCore;

namespace IroneDomeAPI_v1.Services;


public class IronDomeContext : DbContext
{
    public IronDomeContext(DbContextOptions<IronDomeContext> options) : base(options) { }

    public DbSet<Attack> attacks { get; set; }
}