using FluentAssertions;
using Moq;
using HoopInsightsAPI.Clients;
using HoopInsightsAPI.Models;
using HoopInsightsAPI.Services;

namespace HoopInsightsAPI.Tests.Services
{
    public class PlayerServiceTests
    {
        private readonly Mock<IBalldontlieClient> _clientMock;
        private readonly PlayerService _playerService;

        public PlayerServiceTests()
        {
            _clientMock = new Mock<IBalldontlieClient>();
            _playerService = new PlayerService(_clientMock.Object);
        }

        [Fact]
        public async Task GetPlayersAsync_NoSearch_ReturnsAllPlayersAndMeta()
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
                .Setup(c => c.GetPlayersJsonAsync(
                    null,
                    100,
                    null))
                .ReturnsAsync(jsonResponse);

            // Act
            var result = await _playerService.GetPlayersAsync();

            // Assert: data
            result.Data.Should().HaveCount(2, "the JSON payload contains two players");
            result.Data.First().Should().BeEquivalentTo(new PlayerDto
            {
                Id = 1,
                FirstName = "Jaylen",
                LastName = "Brown",
                Position = "G",
                Height = "6-6",
                Weight = "223",
                JerseyNumber = "7",
                College = "California",
                Country = "USA",
                DraftYear = 2016,
                DraftRound = 1,
                DraftNumber = 3,
                Team = new TeamDto
                {
                    Id = 2,
                    Conference = "East",
                    Division = "Atlantic",
                    City = "Boston",
                    Name = "Celtics",
                    FullName = "Boston Celtics",
                    Abbreviation = "BOS"
                }
            }, "the first JSON object maps correctly to PlayerDto");

            // Assert: meta
            result.Meta.PerPage.Should().Be(100, "the JSON per_page is 100");
            result.Meta.NextPage.Should().BeNull("no next_cursor was returned");
            result.Meta.PreviousPage.Should().BeNull("no prev_cursor was returned");
        }

        [Fact]
        public async Task GetPlayersAsync_WithSearch_ReturnsOnlyMatchingPlayersAndMeta()
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
                .Setup(c => c.GetPlayersJsonAsync(
                    "jalen",
                    100,
                    null))
                .ReturnsAsync(jsonResponse);

            // Act
            var result = await _playerService.GetPlayersAsync("jalen");

            // Assert: data
            result.Data.Should().HaveCount(2, "two players match the search term");
            result.Data.Select(p => p.LastName)
                .Should().BeEquivalentTo(new[] { "Brunson", "Rose" },
                    "those players match 'jalen'");

            // Assert: meta
            result.Meta.PerPage.Should().Be(100, "the JSON per_page is 100");
            result.Meta.NextPage.Should().BeNull("no next_cursor was returned");
            result.Meta.PreviousPage.Should().BeNull("no prev_cursor was returned");
        }
    }
}
