using System.Text.Json.Serialization;

namespace HoopInsightsApi.Models;

public class GameData
{
    public int Id { get; set; }
    
    public string Date { get; set; }
    
    public int Season { get; set; }
    
    public string Status { get; set; }
    
    public int Period { get; set; }
    
    public object Time { get; set; }
    
    public bool Postseason { get; set; }
    
    [JsonPropertyName("home_team_score")]
    public int HomeTeamScore { get; set; }
    
    [JsonPropertyName("visitor_team_score")]
    public int VisitorTeamScore { get; set; }
    
    public object DateTime { get; set; }
    
    [JsonPropertyName("home_team")]
    public TeamData HomeTeam { get; set; }
    
    [JsonPropertyName("visitor_team")]
    public TeamData VisitorTeam { get; set; }
}