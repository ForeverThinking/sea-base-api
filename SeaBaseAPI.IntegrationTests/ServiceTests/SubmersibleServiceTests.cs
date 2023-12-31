﻿using FluentAssertions;

namespace SeaBaseAPI.IntegrationTests.ServiceTests;

public sealed class SubmersibleServiceTests : TestUsingSqlite
{
    private readonly SubmersibleService _underTest;

    public SubmersibleServiceTests()
    {
        _underTest = new SubmersibleService(Context);
    }

    [Fact]
    public async Task AddSubmersibleAsync_PostDto_SavesToDb()
    {
        // Arrange
        var dto = new SubmersibleDto()
        {
            VesselName = "Test",
            IsDeployed = false,
            Pilot = null,
            Crew = null,
            Condition = 1.0
        };

        // Act
        await _underTest.AddSubmersibleAsync(dto);

        // Assert
        Context.Submersibles.Should().HaveCount(1);
        Context.Submersibles.SingleOrDefault(s => s.VesselName == dto.VesselName).Should().NotBeNull();
    }

    [Fact]
    public async Task GetAllSubmersiblesAsync_Called_ReturnsCollectionData()
    {
        // Arrange
        Context.Submersibles.AddRange(Enumerable.Range(1, 5).Select(x => new Submersible()
        {
            VesselName = "Test {x}",
            IsDeployed = false,
            Pilot = null,
            Crew = null,
            Condition = 0.98
        }));

        await Context.SaveChangesAsync();

        // Act
        var result = await _underTest.GetAllSubmersiblesAsync();

        // Assert
        result.Should().HaveCount(5);
    }
}
