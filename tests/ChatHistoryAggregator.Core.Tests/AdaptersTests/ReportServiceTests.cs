
using FluentAssertions;
using Moq;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using ChatAggregator.Core.UseCase.InterfaceAdapters;
using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Entities;
using ChatAggregator.Domain.Enums;

namespace ChatHistoryAggregator.Core.Tests.AdaptersTests;

public class ReportServiceTests
{
    private readonly IReportService _reportService;
    private readonly Mock<IEventService> _eventServiceMock;

    public ReportServiceTests()
    {
        _eventServiceMock = new Mock<IEventService>();
        _reportService = new ReportService(_eventServiceMock.Object);
    }

    [Fact]
    public void RenderReport_Should_Return_Minutely_Report_When_Granularity_Is_Minute()
    {
        // Arrange
        var chatEvents = new List<ChatEvent>
        {
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 10, 0), Type = EventType.Comment, UserName = "User3", Message = "Hello!" },
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 10, 0), Type = EventType.Enter, UserName = "User1" },
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 10, 0), Type = EventType.Leave, UserName = "User2" }
        };

        _eventServiceMock.Setup(er => er.GetEvents()).Returns(chatEvents);

        // Act
        var report = _reportService.RenderReport(Granularity.Minute);

        // Assert
        report.Should().Contain("User1 enters the room");
        report.Should().Contain("User2 leaves");
        report.Should().Contain("User3 comments: \"Hello!\"");
    }

    [Fact]
    public void RenderReport_Should_Return_Hourly_Report_When_Granularity_Is_Hourly()
    {
        // Arrange
        var chatEvents = new List<ChatEvent>
        {
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 10, 0), Type = EventType.Comment, UserName = "User3", Message = "Hello!" },
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 20, 0), Type = EventType.Enter, UserName = "User1" },
            new ChatEvent { Time = new DateTime(2023, 5, 15, 6, 20, 0), Type = EventType.Leave, UserName = "User2" }
        };

        _eventServiceMock.Setup(er => er.GetEvents()).Returns(chatEvents);

        // Act
        var report = _reportService.RenderReport(Granularity.Hourly);

        // Assert
        report.Should().Contain("1 person(s) entered");
        report.Should().Contain("1 person(s) left");
        report.Should().Contain("1 comment(s)");
    }

    [Fact]
    public void RenderReport_Should_Return_Daily_Report_When_Granularity_Is_Daily()
    {
        // Arrange
        var chatEvents = new List<ChatEvent>
        {
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 10, 0), Type = EventType.Comment, UserName = "User3", Message = "Hello!" },
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 20, 0), Type = EventType.Enter, UserName = "User1" },
            new ChatEvent { Time = new DateTime(2023, 5, 16, 6, 20, 0), Type = EventType.Leave, UserName = "User2" }
        };

        _eventServiceMock.Setup(er => er.GetEvents()).Returns(chatEvents);

        // Act
        var report = _reportService.RenderReport(Granularity.Daily);

        // Assert
        report.Should().Contain("1 person(s) entered");
        report.Should().Contain("1 person(s) left");
        report.Should().Contain("1 comment(s)");
    }
}
