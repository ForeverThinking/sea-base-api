﻿using FluentAssertions;

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
}
