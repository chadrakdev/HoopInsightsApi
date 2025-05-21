using System.Text.Json;
using HoopInsightsAPI.Clients;
using HoopInsightsAPI.Models;

namespace HoopInsightsAPI.Services;

public class TeamService : ITeamService
{
    private readonly IBalldontlieClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public TeamService(IBalldontlieClient client)
    {
        _client = client;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    
    public async Task<IEnumerable<TeamDto>> GetTeamsAsync(string? name = null)
    {
        var teamJson = await _client.GetTeamsJsonAsync(name ?? string.Empty);

        var teamResponse = JsonSerializer.Deserialize<TeamsResponseDto>(teamJson, _jsonOptions)
                           ?? throw new InvalidOperationException("Failed to parse response from teams endpoint.");

        return teamResponse.Data;
    }
}