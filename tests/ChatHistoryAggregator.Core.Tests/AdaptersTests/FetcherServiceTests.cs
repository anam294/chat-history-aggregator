using ChatAggregator.Core.UseCase.InterfaceAdapters;
using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Enums;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChatHistoryAggregator.Core.Tests.AdaptersTests
{
    public class FetcherServiceTests
    {
        private readonly IFetcherService _fetcherService;
        private readonly Mock<IReportService> _reportServiceMock;

        public FetcherServiceTests()
        {
            _reportServiceMock = new Mock<IReportService>();
            _fetcherService = new FetcherService(_reportServiceMock.Object);
        }

        [Fact]
        public void Fetch_Should_Return_Expected_String_From_ReportService()
        {
            // Arrange
            var granularity = "minute";
            var expectedReport = "Report for minute granularity";
            _reportServiceMock.Setup(r => r.RenderReport(Granularity.Minute)).Returns(expectedReport);

            // Act
            var result = _fetcherService.Fetch(granularity);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedReport);
        }

        [Fact]
        public void Fetch_Should_Throw_Exception_When_Invalid_Granularity_Passed()
        {
            // Arrange
            var invalidGranularity = "invalid";

            // Act
            var act = new System.Action(() => _fetcherService.Fetch(invalidGranularity));

            // Assert
            act.Should().Throw<Exception>().WithMessage("Invalid granularity passed.");
        }

        [Fact]
        public void Fetch_Should_Call_RenderReport_Method_From_ReportService_Once()
        {
            // Arrange
            var granularity = "hourly";

            // Act
            var result = _fetcherService.Fetch(granularity);

            // Assert
            _reportServiceMock.Verify(r => r.RenderReport(Granularity.Hourly), Times.Once);
        }
    }
}
