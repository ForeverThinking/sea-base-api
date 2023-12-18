using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SeaBaseAPI;

namespace SeaBaseAPIUnitTests.ControllerTests;

public sealed class PersonnelControllerTests
{
    private readonly IPersonnelService _personnelServiceSub = Substitute.For<IPersonnelService>();
    private readonly PersonnelController _underTest;

    public PersonnelControllerTests()
    {
        _underTest = new PersonnelController(_personnelServiceSub);
    }

    [Fact]
    public async Task GetAllPersonnel_Called_ReturnsEnumeratedData()
    {
        // Arrange
        List<PersonnelDto> personnelDtos = new()
        {
            new() { Name = "Test1", Department = Department.Research, IsDeployed = false },
            new() { Name = "Test2", Department = Department.IT, IsDeployed = true },
        };

        _personnelServiceSub.GetAllPersonnelAsync().Returns(personnelDtos);

        // Act
        var result = await _underTest.GetAllPersonnel();

        // Assert
        var okResult = result as OkObjectResult;
        var data = okResult?.Value as IEnumerable<PersonnelDto>;

        data.Should().HaveCount(2);
    }

    [Fact]
    public async Task AddPersonnel_PostWithValidModel_Returns200Ok()
    {
        // Arrange
        var dto = new PersonnelDto()
        {
            Name = "test1",
            Department = Department.Maintenance,
            IsDeployed = true,
        };
    
        // Act
        var result = await _underTest.AddPersonnel(dto);
    
        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task AddPersonnel_PostWithInvalidModel_Returns400BadRequest()
    {
        // Arrange
        var dto = new PersonnelDto()
        {
            Name = "test1",
            Department = Department.Maintenance,
            IsDeployed = true,
        };

        _underTest.ModelState.AddModelError("test", "test");

        // Act
        var result = await _underTest.AddPersonnel(dto);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task DeletePersonnel_PostWithValidModel_Returns200Ok()
    {
        // Arrange
        int testId = 1;

        _personnelServiceSub.DeletePersonnelAsync(Arg.Any<int>()).Returns(true);

        // Act
        var result = await _underTest.DeletePersonnel(testId);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task DeletePersonnel_PostWithInalidModel_Returns400BadRequest()
    {
        // Arrange
        int testId = 1;

        _personnelServiceSub.DeletePersonnelAsync(Arg.Any<int>()).Returns(false);

        // Act
        var result = await _underTest.DeletePersonnel(testId);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }
}
