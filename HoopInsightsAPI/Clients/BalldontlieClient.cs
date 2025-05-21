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
}