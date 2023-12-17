using Microsoft.EntityFrameworkCore;

namespace SeaBaseAPI;

public class SeaBaseContext : DbContext
{
    public SeaBaseContext(DbContextOptions<SeaBaseContext> options)
    : base(options)
    {
    }

    public DbSet<Personnel> Personnel => Set<Personnel>();
    public DbSet<Submersible> Submersibles => Set<Submersible>();
}
