using System.Text.Json.Serialization;

namespace HoopInsightsAPI.Models;

public class TeamDto
{
    public int Id { get; set; }
    public string Conference { get; set; }
    public string Division { get; set; }
    public string City { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Abbreviation { get; set; }
}