using HoopInsightsAPI.Extensions;

namespace HoopInsightsAPI.Clients;

public class BalldontlieClient : IBalldontlieClient
{
    private readonly HttpClient _httpClient;

    public BalldontlieClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> GetTeamsJsonAsync(string name = "")
    {
        var url = string.IsNullOrWhiteSpace(name)
            ? "teams"
            : $"teams?search={name.NormaliseForFuzzySearch()}";
        
        using var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}