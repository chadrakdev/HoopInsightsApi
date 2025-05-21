using System.Text.Json;
using HoopInsightsAPI.Clients;
using HoopInsightsAPI.Models;

namespace HoopInsightsAPI.Services;

public class TeamService : ITeamService
{
    private readonly IBalldontlieClient _client;

    public TeamService(IBalldontlieClient client)
    {
        _client = client;
    }
    
    public async Task<IEnumerable<TeamDto>> GetTeamsAsync(string? name = null)
    {
        var teamJson = await _client.GetTeamsJsonAsync(name ?? string.Empty);

        var teamResponse = JsonSerializer.Deserialize<TeamsResponseDto>(teamJson)
                           ?? throw new InvalidOperationException("Failed to parse response from teams endpoint.");

        return teamResponse.Data;
    }
}