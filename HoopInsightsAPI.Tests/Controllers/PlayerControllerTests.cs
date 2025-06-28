using FluentAssertions;
using HoopInsightsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using HoopInsightsAPI.Models;
using HoopInsightsAPI.Services;

namespace HoopInsightsAPI.Tests.Controllers
{
    public class PlayerControllerTests
    {
        private readonly Mock<IPlayerService> _serviceMock;
        private readonly PlayerController _controller;

        public PlayerControllerTests()
        {
            _serviceMock = new Mock<IPlayerService>();
            _controller = new PlayerController(_serviceMock.Object);
        }

        [Fact]
        public async Task Get_WithResults_ReturnsOkWithDataAndMeta()
        {
            // Arrange
            var dto = new PlayersResponseDto
            {
                Data = new List<PlayerDto>
                {
                    new PlayerDto { Id = 1, FirstName = "James", LastName = "Harden" }
                },
                Meta = new MetaDto { PerPage = 100, NextPage = 123456, PreviousPage = null }
            };
            _serviceMock
                .Setup(s => s.GetPlayersAsync("james", 100, null))
                .ReturnsAsync(dto);

            // Act
            var result = await _controller.GetPlayers(search: "james", perPage: 100, cursor: null);

            // Assert
            var ok = result.Should().BeOfType<OkObjectResult>().Subject;
            ok.Value.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public async Task Get_WithNoResults_ReturnsNotFound()
        {
            // Arrange
            var emptyDto = new PlayersResponseDto
            {
                Data = new List<PlayerDto>(),
                Meta = new MetaDto { PerPage = 100 }
            };
            _serviceMock
                .Setup(s => s.GetPlayersAsync(null, 100, null))
                .ReturnsAsync(emptyDto);

            // Act
            var result = await _controller.GetPlayers(search: null, perPage: 100, cursor: null);

            // Assert
            var notFound = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFound.Value.Should().BeEquivalentTo(new { error = "No players found.", status = 404 });
        }
    }
}
