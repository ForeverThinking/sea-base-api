using FluentAssertions;

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
}
