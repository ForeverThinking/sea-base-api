using System.Data.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace SeaBaseAPI.UnitTests.ControllerTests;

public sealed class SubmersibleControllerTests
{
    private readonly ISubmersibleService _submersibleServiceSub = Substitute.For<ISubmersibleService>();
    private readonly SubmersibleController _underTest;

    public SubmersibleControllerTests()
    {
        _underTest = new SubmersibleController(_submersibleServiceSub);
    }

    [Fact]
    public async Task AddSubmersible_PostWithValidModel_Returns201Created()
    {
        // Arrange
        var dto = new SubmersibleDto
        {
            VesselName = "test",
            IsDeployed = false,
            Pilot = null,
            Crew = null,
            Condition = 1.0
        };

        var dbResult = new Submersible
        {
            Id = 1,
            VesselName = "test",
            IsDeployed = false,
            Pilot = null,
            Crew = null,
            Condition = 1.0
        };

        _submersibleServiceSub.AddSubmersibleAsync(Arg.Any<SubmersibleDto>()).Returns(dbResult);

        // Act
        var result = await _underTest.AddSubmersible(dto);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task AddSubmersible_PostWithInvalidModel_Returns400BadRequest()
    {
        // Arrange
        var dto = new SubmersibleDto
        {
            VesselName = "test",
            IsDeployed = false,
            Pilot = null,
            Crew = null,
            Condition = 100
        };

        _underTest.ModelState.AddModelError("test", "test");

        // Act
        var result = await _underTest.AddSubmersible(dto);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task GetAllSubmersibles()
    {
        // Arrange
        List<SubmersibleDto> submersibles = new()
        {
            new() { VesselName = "Test 1", IsDeployed = false, Pilot = null, Crew = null, Condition = 1.0 },
            new() { VesselName = "Test 2", IsDeployed = true, Pilot = null, Crew = null, Condition = 0.95 }
        };

        _submersibleServiceSub.GetAllSubmersiblesAsync().Returns(submersibles);

        // Act
        var result = await _underTest.GetAllSubmersibles();

        // Assert
        var okResult = result as OkObjectResult;
        var data = okResult?.Value as IEnumerable<SubmersibleDto>;

        data.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetSubmersible_GetWithValidId_ReturnsOkWithResult()
    {
        // Arrange
        int testId = 1;

        var submersibleDto = new SubmersibleDto
        {
            VesselName = "test",
            IsDeployed = false,
            Pilot = null,
            Crew = null,
            Condition = 100
        };

        _submersibleServiceSub.GetSingleSubmersibleAsync(Arg.Any<int>()).Returns(submersibleDto);

        // Act
        var result = await _underTest.GetSubmersible(testId);

        // Assert
        var okResult = result as OkObjectResult;
        var data = okResult?.Value as SubmersibleDto;

        data.Should().BeEquivalentTo(submersibleDto);
    }

    [Fact]
    public async Task GetSubmersible_GetWithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int testId = 1;

        _submersibleServiceSub.GetSingleSubmersibleAsync(Arg.Any<int>()).ReturnsNull();

        // Act
        var result = await _underTest.GetSubmersible(testId);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateSubmersible_PutWithValidModel_ReturnsOk()
    {
        // Arrange
        int testId = 1;
        SubmersibleDto testDto = new SubmersibleDto();

        _submersibleServiceSub.UpdateSubmersibleAsync(Arg.Any<int>(), Arg.Any<SubmersibleDto>()).Returns(true);

        // Act
        var result = await _underTest.UpdateSubmersible(testId, testDto);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task UpdateSubmersible_PutWithInvalidModel_ReturnsBadRequest()
    {
        // Arrange
        int testId = 1;
        SubmersibleDto testDto = new SubmersibleDto();

        _submersibleServiceSub.UpdateSubmersibleAsync(Arg.Any<int>(), Arg.Any<SubmersibleDto>()).Returns(false);

        // Act
        var result = await _underTest.UpdateSubmersible(testId, testDto);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task DeletePersonnel_PostWithValidModel_Returns200Ok()
    {
        // Arrange
        int testId = 1;

        _submersibleServiceSub.DeleteSubmersibleAsync(Arg.Any<int>()).Returns(true);

        // Act
        var result = await _underTest.DeleteSubmersible(testId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeletePersonnel_PostWithInalidModel_Returns400BadRequest()
    {
        // Arrange
        int testId = 1;

        _submersibleServiceSub.DeleteSubmersibleAsync(Arg.Any<int>()).Returns(false);

        // Act
        var result = await _underTest.DeleteSubmersible(testId);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }
}
