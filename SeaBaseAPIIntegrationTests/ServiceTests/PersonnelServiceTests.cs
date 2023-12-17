using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeaBaseAPI;

namespace SeaBaseAPIIntegrationTests.ServiceTests;

public sealed class PersonnelServiceTests : IDisposable
{
    private readonly SeaBaseContext _context;
    private readonly IPersonnelService _undertest;

    public PersonnelServiceTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<PersonnelServiceTests>()
            .AddEnvironmentVariables()
            .Build();

        var _connection = configuration.GetValue<string>("dbString");

        var options = new DbContextOptionsBuilder<SeaBaseContext>().
            UseNpgsql(connectionString: _connection)
            .Options;
        
        _context = new SeaBaseContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.Migrate();

        _undertest = new PersonnelService(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task GetAllPersonnelAsync_Called_ReturnsEnumeratedData()
    {
        // Arrange
        _context.Personnel.AddRange(Enumerable.Range(1, 5).Select( x => new Personnel()
        {
            Name = "Test {x}",
            Department = Department.Logistics,
            IsDeployed = false
        }));

        await _context.SaveChangesAsync();

        // Act
        var result = await _undertest.GetAllPersonnelAsync();

        // Assert
        result.Should().HaveCount(5);
    }
}
