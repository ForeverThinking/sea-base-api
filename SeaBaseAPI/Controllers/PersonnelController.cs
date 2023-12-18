using Microsoft.AspNetCore.Mvc;

namespace SeaBaseAPI;

[ApiController]
[Route("[controller]/[action]")]
public sealed class PersonnelController : ControllerBase
{
    private readonly IPersonnelService _personnelService;
    public PersonnelController(IPersonnelService personnelService)
    {
        _personnelService = personnelService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPersonnel()
    {
        var personnel = await _personnelService.GetAllPersonnelAsync();

        return Ok(personnel);
    }

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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePersonnel([FromBody] int id) 
        => await _personnelService.DeletePersonnelAsync(id) ? Ok() : BadRequest();
}
