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

    [Fact]
    public async Task DeletePersonnelAsync_PostId_ReturnsTrue()
    {
        // Arrange
        var personnel = new Personnel()
        {
            Id = 1,
            Name = "Test 1",
            Department = Department.Logistics,
            IsDeployed = false,
        };

        Context.Personnel.Add(personnel);

        // Act
        var result = await _underTest.DeletePersonnelAsync(1);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeletePersonnelAsync_PostInvalidId_ReturnsFalse()
    {
        // Arrange
        var personnel = new Personnel()
        {
            Id = 1,
            Name = "Test 1",
            Department = Department.Logistics,
            IsDeployed = false,
        };

        Context.Personnel.Add(personnel);

        // Act
        var result = await _underTest.DeletePersonnelAsync(2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetPersonAsync_UsingValidId_ReturnsDto()
    {
        // Arrange
        var testId = 1;

        var personnel = new Personnel()
        {
            Id = 1,
            Name = "Test 1",
            Department = Department.Logistics,
            IsDeployed = false,
        };

        Context.Personnel.Add(personnel);

        // Act
        var result = await _underTest.GetPersonAsync(testId);

        // Assert
        result.Should().BeOfType<PersonnelDto>();
    }

    [Fact]
    public async Task GetPersonAsync_UsingInvalidId_ReturnsNull()
    {
        // Arrange
        var testInvalidId = 100;

        var personnel = new Personnel()
        {
            Id = 1,
            Name = "Test 1",
            Department = Department.Logistics,
            IsDeployed = false,
        };

        Context.Personnel.Add(personnel);

        // Act
        var result = await _underTest.GetPersonAsync(testInvalidId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdatePersonnelAsync_UsingValidId_ReturnsTrue()
    {
        // Arrange
        var testId = 1;

        var personnel = new Personnel()
        {
            Id = 1,
            Name = "Test 1",
            Department = Department.Logistics,
            IsDeployed = false,
        };

        Context.Personnel.Add(personnel);
        await Context.SaveChangesAsync();

        var updatePersonnel = new PersonnelDto()
        {
            Name = "Test 2",
            Department = Department.Research,
            IsDeployed = false,
        };

        // Act
        var result = await _underTest.UpdatePersonnelAsync(testId, updatePersonnel);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatePersonnelAsync_UsingInvalidId_ReturnsFalse()
    {
        // Arrange
        var testId = 5;

        var personnel = new Personnel()
        {
            Id = 1,
            Name = "Test 1",
            Department = Department.Logistics,
            IsDeployed = false,
        };

        Context.Personnel.Add(personnel);
        await Context.SaveChangesAsync();

        var updatePersonnel = new PersonnelDto()
        {
            Name = "Test 2",
            Department = Department.Research,
            IsDeployed = false,
        };

        // Act
        var result = await _underTest.UpdatePersonnelAsync(testId, updatePersonnel);

        // Assert
        result.Should().BeFalse();
    }
}
