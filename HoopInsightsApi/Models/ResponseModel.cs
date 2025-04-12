using System.Text.Json;

namespace HoopInsightsApi.Models;

public class ResponseModel
{
    public JsonElement[] Data { get; set; }
}