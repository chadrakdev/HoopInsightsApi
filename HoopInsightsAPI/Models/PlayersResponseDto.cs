namespace HoopInsightsAPI.Models;

public class PlayersResponseDto
{
    public IEnumerable<PlayerDto> Data { get; set; }
    public MetaDto Meta { get; set; }
}