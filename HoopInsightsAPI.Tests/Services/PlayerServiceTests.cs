using HoopInsightsAPI.Clients;
using Moq;

namespace HoopInsightsAPI.Tests.Services;

public class PlayerServiceTests
{
    private readonly Mock<IBalldontlieClient> _clientMock;
    private readonly PlayerService _pLayerService;

    public PLayerServiceTests()
    {
        _clientMock = new Mock<IBalldontlieClient>();
        _pLayerService = new PlayerService(_clientMock.Object);
    }

    [Fact]
    public async Task GetPlayersAsync_WithNoSearch_ReturnsAllPlayers()
    {
        // Arrange
        var jsonResponse = """
        {
          "data": [
            {
              "id": 1,
              "first_name": "Jaylen",
              "last_name": "Brown",
              "position": "G",
              "height": "6-6",
              "weight": "223",
              "jersey_number": "7",
              "college": "California",
              "country": "USA",
              "draft_year": 2016,
              "draft_round": 1,
              "draft_number": 3,
              "team": {
                "id": 2,
                "conference": "East",
                "division": "Atlantic",
                "city": "Boston",
                "name": "Celtics",
                "full_name": "Boston Celtics",
                "abbreviation": "BOS"
              }
            },
            {
              "id": 2,
              "first_name": "Jayson",
              "last_name": "Tatum",
              "position": "F",
              "height": "6-8",
              "weight": "210",
              "jersey_number": "0",
              "college": "Duke",
              "country": "USA",
              "draft_year": 2017,
              "draft_round": 1,
              "draft_number": 3,
              "team": {
                "id": 2,
                "conference": "East",
                "division": "Atlantic",
                "city": "Boston",
                "name": "Celtics",
                "full_name": "Boston Celtics",
                "abbreviation": "BOS"
              }
            }
          ],
          "meta": {
            "per_page": 100
          }
        }
        """;

        _clientMock
          .Setup(client => client.GetPlayersJsonAsync(string.Empty, 100))
          .ReturnsAsync(jsonResponse);

        // Act
        var players = await _pLayerService.GetPlayersAsync();

        // Assert
        players.Should().HaveCount(2, because: "the JSON payload contains two players");
        players.First().Should().Match<PlayerDto>(player =>
          player.ID == 1 &&
          player.FirstName == "Jaylen" &&
          player.LastName == "Brown" &&
          player.Position == "G" &&
          player.Height == "6-6" &&
          player.Weight == "223" &&
          player.JerseyNumber == "7" &&
          player.College == "California" &&
          player.Country == "USA"
        );
        players.First().Should().Match<PlayerDto>(player =>
          player.ID == 2 &&
          player.FirstName == "Jayson" &&
          player.LastName == "Tatum" &&
          player.Position == "F" &&
          player.Height == "6-8" &&
          player.Weight == "210" &&
          player.JerseyNumber == "0" &&
          player.College == "Duke" &&
          player.Country == "USA"
        );
    }

    [Fact]
    public async Task GetPlayers_WithSearch_ReturnsOnlyMatchingPlayers()
    {
      // Arrange
      var jsonResponse = """
      {
        "data": [
          {
            "id": 73,
            "first_name": "Jalen",
            "last_name": "Brunson",
            "position": "G",
            "height": "6-2",
            "weight": "190",
            "jersey_number": "11",
            "college": "Villanova",
            "country": "USA",
            "draft_year": 2018,
            "draft_round": 2,
            "draft_number": 33,
            "team": {
              "id": 20,
              "conference": "East",
              "division": "Atlantic",
              "city": "New York",
              "name": "Knicks",
              "full_name": "New York Knicks",
              "abbreviation": "NYK"
            }
          },
          {
            "id": 874,
            "first_name": "Jalen",
            "last_name": "Rose",
            "position": "",
            "height": "6-8",
            "weight": "215",
            "jersey_number": "8",
            "college": "Michigan",
            "country": "USA",
            "draft_year": 1994,
            "draft_round": 1,
            "draft_number": 13,
            "team": {
              "id": 12,
              "conference": "East",
              "division": "Central",
              "city": "Indiana",
              "name": "Pacers",
              "full_name": "Indiana Pacers",
              "abbreviation": "IND"
            }
          }
        ],
        "meta": {
          "per_page": 100
        }
      }
      """;

      _clientMock
        .Setup(client => client.GetPlayersJsonAsync("jalen", 100))
        .ReturnsAsync(jsonResponse);

      // Act
      var players = await _pLayerService.GetPlayersAsync("jalen", 100);

      // Assert
      players.Should().HaveCount(2, because: "only Brunson and Rose match the search term in the JSON");
      players.Select(player => player.LastName)
        .Should().BeEquivalentTo(new[] { "Brunson", "Rose" }, 
          because: "those are the players whose names include the search token 'jalen'");
    }
}