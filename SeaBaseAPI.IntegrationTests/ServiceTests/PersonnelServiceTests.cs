using FluentAssertions;
using SeaBaseAPI;

namespace SeaBaseAPIIntegrationTests.ServiceTests;

public sealed class PersonnelServiceTests : IClassFixture<ServiceTestsBase>, IDisposable
{
    private readonly SeaBaseContext _context;
    private readonly PersonnelService _underTest;

    public PersonnelServiceTests(ServiceTestsBase testsBase)
    {
        _context = testsBase.DbContext;
        _context.Database.BeginTransaction();

        _underTest = new PersonnelService(_context);
    }

    public void Dispose()
    {
        _context.Database.RollbackTransaction();
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
        var result = await _underTest.GetAllPersonnelAsync();

        // Assert
        result.Should().HaveCount(5);
    }

    [Fact]
    public async Task AddPersonnelAsync_PostDto_AddsToDb()
    {
        // Arrange
        var dto = new PersonnelDto()
        {
            Name = "Test",
            Department = Department.Submersible,
            IsDeployed = true,
        };

        // Act
        await _underTest.AddPersonnelAsync(dto);

        // Assert
        _context.Personnel.Should().HaveCount(1);
        _context.Personnel.SingleOrDefault(p => p.Name == dto.Name).Should().NotBeNull();
    }
}
