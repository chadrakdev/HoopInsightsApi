using HoopInsightsAPI.Models;

namespace HoopInsightsAPI.Services;

public interface IPlayerService
{
    Task<PlayersResponseDto> GetPlayersAsync(string? search = null, int perPage = 100, string? cursor = null);
}