using HoopInsightsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HoopInsightsAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlayers([FromQuery] string? search, [FromQuery] int perPage = 100,
        [FromQuery] string? cursor = null)
    {
        var result = await _playerService.GetPlayersAsync(search, perPage, cursor);

        if (!result.Data.Any()) return NotFound(new { error = "No players found.", status = 404 });

        return Ok(result);
    }
}