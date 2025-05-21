using HoopInsightsAPI.Models;
using HoopInsightsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HoopInsightsAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeams([FromQuery]string? name)
    {
        var teams = await _teamService.GetTeamsAsync(name);

        if (!teams.Any()) return NotFound(new { status = 404, error = "No teams found." });

        return Ok(teams);
    }
}