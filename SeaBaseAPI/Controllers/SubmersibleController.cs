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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSubmersible([FromBody] SubmersibleDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _submersibleService.AddSubmersibleAsync(dto);
        return Ok();
    }
}
