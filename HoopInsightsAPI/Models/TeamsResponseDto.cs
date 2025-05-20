namespace HoopInsightsAPI.Models;

public class TeamsResponseDto
{
    public IEnumerable<TeamDto> Data { get; init; } = new List<TeamDto>();
}