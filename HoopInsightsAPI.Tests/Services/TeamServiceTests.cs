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
}