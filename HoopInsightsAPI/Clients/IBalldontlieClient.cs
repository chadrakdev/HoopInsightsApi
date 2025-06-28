namespace HoopInsightsAPI.Clients;

public interface IBalldontlieClient
{
    Task<string> GetTeamsJsonAsync();
    
    Task<string> GetPlayersJsonAsync(string? search = null, int perPage = 100, string? cursor = null);
}