using FluentAssertions;
using HoopInsightsAPI.Clients;
using HoopInsightsAPI.Services;
using Moq;

namespace HoopInsightsAPI.Tests.Services;

public class TeamServiceTests
{
    private readonly Mock<IBalldontlieClient> _clientMock;
    private readonly TeamService _teamService;

    public TeamServiceTests()
    {
        _clientMock = new Mock<IBalldontlieClient>();
        _teamService = new TeamService(_clientMock.Object);
    }

    [Fact]
    public async Task GetTeamsAsync_WithName_ShouldReturnMappedTeamDto()
    {
        // Arrange
        var jsonResponse = """
           {
             "data": [
               {
                 "id": 14,
                 "abbreviation": "MIN",
                 "city": "Minnesota",
                 "conference": "West",
                 "division": "Northwest",
                 "full_name": "Minnesota Timberwolves",
                 "name": "Timberwolves"
               }
             ]
           }
       """;
        
        _clientMock
            .Setup(client => client.GetTeamsJsonAsync())
            .ReturnsAsync(jsonResponse);

        // Act
        var teams = await _teamService.GetTeamsAsync("minnesota");
        
        // Assert
        teams.Should().HaveCount(1, because: "the JSON contains exactly one team");

        var team = teams.First();
        team.FullName.Should().Be("Minnesota Timberwolves", because: "that is the full_name value in the JSON");
        team.Id.Should().Be(14, because: "the id is 14 in the JSON");
        team.Abbreviation.Should().Be("MIN", because: "the abbreviation is MIN in the JSON");
    }

    [Fact]
    public async Task GetTeamsAsync_WithPartialName_ShouldReturnOnlyMatchingTeams()
    {
        // Arrange
        var jsonResponse = """
           {
             "data": [
               {
                 "id": 14,
                 "abbreviation": "MIN",
                 "city": "Minnesota",
                 "conference": "West",
                 "division": "Northwest",
                 "full_name": "Minnesota Timberwolves",
                 "name": "Timberwolves"
               },
               {
                 "id": 2,
                 "abbreviation": "BOS",
                 "city": "Boston",
                 "conference": "East",
                 "division": "Atlantic",
                 "full_name": "Boston Celtics",
                 "name": "Celtics"
               }
             ]
           }
       """;

        _clientMock
            .Setup(client => client.GetTeamsJsonAsync())
            .ReturnsAsync(jsonResponse);

        // Act
        var teams = await _teamService.GetTeamsAsync("wolves");

        // Assert
        teams.Should().ContainSingle(
            team => team.FullName == "Minnesota Timberwolves",
            because: "only the Timberwolves full_name contains 'wolves'");
    }
}