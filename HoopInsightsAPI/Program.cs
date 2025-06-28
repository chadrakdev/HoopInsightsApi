using HoopInsightsAPI.Clients;
using HoopInsightsAPI.Middleware;
using HoopInsightsAPI.Services;

namespace HoopInsightsAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<ITeamService, TeamService>();
        builder.Services.AddScoped<IPlayerService, PlayerService>();

        // Make IHTTPContextAccessor available and register custom HTTP handler.
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<ApiKeyForwardingHandler>();

        // Register typed HttpClient and wire up custom handler.
        builder.Services.AddHttpClient<IBalldontlieClient, BalldontlieClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.balldontlie.io/v1/");
        })
        .AddHttpMessageHandler<ApiKeyForwardingHandler>();

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