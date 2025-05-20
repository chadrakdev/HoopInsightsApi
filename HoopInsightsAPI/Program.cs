using HoopInsightsAPI.Middleware;
using HoopInsightsAPI.Services;

namespace HoopInsightsAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<ITeamService, TeamService>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<ApiKeyForwardingHandler>();

        // Add services to the container.

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}