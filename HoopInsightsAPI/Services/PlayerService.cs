using System.Text.Json;
using HoopInsightsAPI.Clients;
using HoopInsightsAPI.Models;

namespace HoopInsightsAPI.Services;

public class PlayerService : IPlayerService
{
    private readonly IBalldontlieClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public PlayerService(IBalldontlieClient client)
    {
        _client = client;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<PlayersResponseDto> GetPlayersAsync(string? search = null, int perPage = 100,
        string? cursor = null)
    {
        var json = await _client.GetPlayersJsonAsync(search, perPage, cursor);

        var result = JsonSerializer.Deserialize<PlayersResponseDto>(json, _jsonOptions)
                     ?? throw new InvalidOperationException("Failed to parse players response.");

        return result;
    }
}