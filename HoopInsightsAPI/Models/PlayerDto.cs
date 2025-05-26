using System.Text.Json.Serialization;

namespace HoopInsightsAPI.Models;

public class PlayerDto
{
    public int Id { get; set; }
    
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    
    public string Position { get; set; }
    
    public string Height { get; set; }
    
    public string Weight { get; set; }
    
    [JsonPropertyName("jersey_number")]
    public string JerseyNumber { get; set; }
    
    public string College { get; set; }
    
    public string Country { get; set; }
    
    [JsonPropertyName("draft_year")]
    public int DraftYear { get; set; }
    
    [JsonPropertyName("draft_round")]
    public int DraftRound { get; set; }
    
    [JsonPropertyName("draft_number")]
    public int DraftNumber { get; set; }
    
    public TeamDto Team { get; set; }
}