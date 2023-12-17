using Microsoft.AspNetCore.Mvc;

namespace SeaBaseAPI;

[ApiController]
[Route("[controller]")]
public sealed class PersonnelController : ControllerBase
{
    private readonly IPersonnelService _personnelService;
    public PersonnelController(IPersonnelService personnelService)
    {
        _personnelService = personnelService;
    }

    [HttpGet]
    public async Task<IEnumerable<PersonnelDto>> GetAllPersonnel()
        =>  await _personnelService.GetAllPersonnelAsync();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPersonnel([FromBody] PersonnelDto personnel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _personnelService.AddPersonnelAsync(personnel);
        return Ok();
    }
}
