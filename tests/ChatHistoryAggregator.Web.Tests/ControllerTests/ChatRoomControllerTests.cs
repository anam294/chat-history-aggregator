using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ChatHistoryAggregator.Web.Tests.ControllerTests
{
    public class ChatRoomControllerTests
    {
        private readonly ChatRoomController _controller;
        private readonly Mock<IFetcherService> _fetcherServiceMock;

        public ChatRoomControllerTests()
        {
            _fetcherServiceMock = new Mock<IFetcherService>();
            _controller = new ChatRoomController(_fetcherServiceMock.Object);
        }

        [Fact]
        public void Get_Should_Return_Fetched_String_From_Service()
        {
            // Arrange
            var granularity = "hourly";
            var expectedFetchedData = "Aggregated data for hourly";
            _fetcherServiceMock.Setup(s => s.Fetch(It.IsAny<string>())).Returns(expectedFetchedData);

            // Act
            var result = _controller.Get(granularity);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedFetchedData);
        }

        [Fact]
        public void Get_Should_Call_Fetch_Method_From_Service_Once()
        {
            // Arrange
            var granularity = "hourly";

            // Act
            var result = _controller.Get(granularity);

            // Assert
            _fetcherServiceMock.Verify(s => s.Fetch(granularity), Times.Once);
        }
    }
}
