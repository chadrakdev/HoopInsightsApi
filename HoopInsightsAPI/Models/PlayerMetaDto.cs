using System.Text.Json.Serialization;

namespace HoopInsightsAPI.Models;

public class PlayerMetaDto
{
    [JsonPropertyName("prev_cursor")]
    public int PreviousPage { get; set; }
    
    [JsonPropertyName("next_cursor")]
    public int NextPage { get; set; }
    
    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
}