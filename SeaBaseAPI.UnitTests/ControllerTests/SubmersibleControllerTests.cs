﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

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
    public async Task AddSubmersible_PostWithValidModel_Returns200Ok()
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

        // Act
        var result = await _underTest.AddSubmersible(dto);

        // Assert
        result.Should().BeOfType<OkResult>();
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
}
