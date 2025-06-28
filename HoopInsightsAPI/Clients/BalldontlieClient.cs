using HoopInsightsAPI.Extensions;

namespace HoopInsightsAPI.Clients;

public class BalldontlieClient : IBalldontlieClient
{
    private readonly HttpClient _httpClient;

    public BalldontlieClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> GetTeamsJsonAsync()
    {
        using var response = await _httpClient.GetAsync("teams");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetPlayersJsonAsync(string? search = null, int perPage = 100, string? cursor = null)
    {
        var query = new List<string>
        {
            $"per_page={perPage}"
        };

        if (!string.IsNullOrWhiteSpace(search))
        {
            var token = search.NormaliseForFuzzySearch();
            query.Add($"search={token}");
        }

        if (!string.IsNullOrWhiteSpace(cursor))
        {
            query.Add($"cursor={Uri.EscapeDataString(cursor)}");
        }

        var url = "players?" + string.Join("&", query);
        using var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }
}