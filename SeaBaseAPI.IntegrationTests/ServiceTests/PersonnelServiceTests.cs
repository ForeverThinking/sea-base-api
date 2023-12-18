using FluentAssertions;

namespace SeaBaseAPI.IntegrationTests.ServiceTests;

public sealed class PersonnelServiceTests : TestUsingSqlite
{
    private readonly PersonnelService _underTest;

    public PersonnelServiceTests()
    {
        _underTest = new PersonnelService(Context);
    }

    [Fact]
    public async Task GetAllPersonnelAsync_Called_ReturnsEnumeratedData()
    {
        // Arrange
        Context.Personnel.AddRange(Enumerable.Range(1, 5).Select( x => new Personnel()
        {
            Name = "Test {x}",
            Department = Department.Logistics,
            IsDeployed = false
        }));

        await Context.SaveChangesAsync();

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
        Context.Personnel.Should().HaveCount(1);
        Context.Personnel.SingleOrDefault(p => p.Name == dto.Name).Should().NotBeNull();
    }
}
