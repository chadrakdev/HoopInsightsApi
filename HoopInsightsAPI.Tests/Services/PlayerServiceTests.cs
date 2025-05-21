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
}