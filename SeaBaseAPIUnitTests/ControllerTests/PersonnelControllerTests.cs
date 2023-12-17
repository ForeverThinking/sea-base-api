using FluentAssertions;
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
        result.Should().HaveCount(2);
    }
}
