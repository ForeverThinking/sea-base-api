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

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePersonnel([FromBody] int id) 
        => await _personnelService.DeletePersonnelAsync(id) ? NoContent() : BadRequest();

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPerson([FromRoute] int id)
    {
        var person = await _personnelService.GetPersonAsync(id);

        if (person is not null)
        {
            return Ok(person);
        }

        return NotFound();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePersonnel([FromRoute] int id, [FromBody] PersonnelDto dto)
        => await _personnelService.UpdatePersonnelAsync(id, dto) ? Ok() : BadRequest();
}
