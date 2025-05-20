using System.Text.Json.Serialization;

namespace HoopInsightsAPI.Models;

public class TeamDto
{
    public int Id { get; set; }
    public string Conference { get; set; }
    public string Division { get; set; }
    public string City { get; set; }
    public string Name { get; set; }
    
    [JsonPropertyName("full_name")]
    public string FullName { get; set; }
    
    [JsonPropertyName("abbreviation")]
    public string Abbreviation { get; set; }
}