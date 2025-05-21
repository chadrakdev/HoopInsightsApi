using FluentAssertions;
using HoopInsightsAPI.Controllers;
using HoopInsightsAPI.Models;
using HoopInsightsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HoopInsightsAPI.Tests.Controllers;

public class TeamControllerTests
{
    private readonly Mock<ITeamService> _serviceMock;
    private readonly TeamController _controller;

    public TeamControllerTests()
    {
        _serviceMock = new Mock<ITeamService>();
        _controller = new TeamController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetTeams_WithResults_ShouldReturnOkWithTeams()
    {
        // Arrange
        var teams = new List<TeamDto>
        {
            new() { Id = 1, Conference = "East", Division = "Southeast", City = "Atlanta", Name = "Hawks", FullName = "Atlanta Hawks", Abbreviation = "ATL"},
            new() { Id = 2, Conference = "East", Division = "Atlantic", City = "Boston", Name = "Celtics", FullName = "Boston Celtics", Abbreviation = "BOS"},
            new() { Id = 3, Conference = "East", Division = "Atlantic", City = "Brooklyn", Name = "Nets", FullName = "Brooklyn Nets", Abbreviation = "BKN"}
        };

        _serviceMock
            .Setup(service => service.GetTeamsAsync("celtics"))
            .ReturnsAsync(teams);

        // Act
        var result = await _controller.GetTeams("minnesota");

        // Assert
        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(teams);
    }
    
    [Fact]
    public async Task Get_WithNoResults_ReturnsNotFound()
    {
        // Arrange
        _serviceMock
            .Setup(service => service.GetTeamsAsync("seattle"))
            .ReturnsAsync(new List<TeamDto>());

        // Act
        var result = await _controller.GetTeams("seattle");

        // Assert
        var notFound = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFound.Value.Should().BeEquivalentTo(new { status = 404, error = "No teams found." });
    }
}