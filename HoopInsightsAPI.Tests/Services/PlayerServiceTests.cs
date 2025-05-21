using HoopInsightsAPI.Clients;
using Moq;

namespace HoopInsightsAPI.Tests.Services;

public class PlayerServiceTests
{
    private readonly Mock<IBalldontlieClient> _clientMock;
    private readonly PLayerService _pLayerService;

    public PLayerServiceTests()
    {
        _clientMock = new Mock<IBalldontlieClient>();
        _pLayerService = new PlayerService(_clientMock.Object);
    }

    [Fact]
    public async Task GetPlayersAsync_WithNoSearch_ShouldReturnAllPlayers()
    {
        // Arrange
        var jsonResponse = """
           {
             "data": [
               { "id": 1, "first_name": "James", "last_name": "Harden" },
               { "id": 2, "first_name": "LeBron", "last_name": "James" }
             ],
             "meta": {
               "total_pages": 1,
               "current_page": 1,
               "next_page": null,
               "per_page": 100,
               "total_count": 2
             }
           }
       """;

        _clientMock
            .Setup(client => client.GetPlayersJsonAsync(
                search: null,
                page: 1,
                perPage: 100))
            .ReturnsAsync(jsonResponse);

        // Act
        var players = await _pLayerService.GetPlayersAsync();
        
        // Assert
        players.Should().HaveCount(2, because: "the JSON payload contains two players");
        players.First().Should().Match<PlayerDto>(player =>
            player.Id == 1 &&
            player.FirstName == "James" &&
            player.LastName == "Harden");
        players.Last().Should().Match<PlayerDto>(player =>
            player.Id == 2 &&
            player.FirstName == "Lebron James" &&
            player.LastName == "James");
    }
}