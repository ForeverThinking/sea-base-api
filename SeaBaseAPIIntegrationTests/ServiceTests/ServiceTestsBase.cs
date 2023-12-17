using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeaBaseAPI.IntegrationTests.ServiceTests;

namespace SeaBaseAPI.IntegrationTests;

public class ServiceTestsBase : IDisposable
{
    public SeaBaseContext DbContext { get; private set; }

    public ServiceTestsBase()
    {
        DbContext = CreateDbContext();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        DbContext.Dispose();
    }

    private static SeaBaseContext CreateDbContext()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<PersonnelServiceTests>()
            .AddEnvironmentVariables()
            .Build();

        var _connection = configuration.GetValue<string>("dbString");

        var options = new DbContextOptionsBuilder<SeaBaseContext>().
            UseNpgsql(connectionString: _connection)
            .Options;

        var context = new SeaBaseContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}
