using HoopInsightsAPI.Models;

namespace HoopInsightsAPI.Services;

public interface ITeamService
{
    Task<IEnumerable<TeamDto>> GetTeamsAsync(string? name = null);
}