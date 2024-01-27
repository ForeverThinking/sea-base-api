using Microsoft.AspNetCore.Mvc;

namespace SeaBaseAPI;

[ApiController]
[Route("[controller]/[action]")]
public sealed class SubmersibleController : ControllerBase
{
    private readonly ISubmersibleService _submersibleService;

    public SubmersibleController(ISubmersibleService submersibleService)
    {
        _submersibleService = submersibleService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSubmersible([FromBody] SubmersibleDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var newEntry = await _submersibleService.AddSubmersibleAsync(dto);
        return CreatedAtAction(
            actionName: nameof(GetSubmersible), 
            routeValues: new { id = newEntry.Id }, 
            value: newEntry);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSubmersibles()
    {
        var submersibles = await _submersibleService.GetAllSubmersiblesAsync();

        return Ok(submersibles);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSubmersible(int id)
    {
        var submersible = await _submersibleService.GetSingleSubmersibleAsync(id);

        if (submersible is not null)
        {
            return Ok(submersible);
        }

        return NotFound();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSubmersible([FromRoute] int id, [FromBody] SubmersibleDto dto)
         => await _submersibleService.UpdateSubmersibleAsync(id, dto) ? Ok() : BadRequest();
}
