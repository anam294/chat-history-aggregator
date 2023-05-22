using ChatAggregator.Core.UseCase.InterfaceAdapters;
using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Entities;
using ChatAggregator.Domain.Enums;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChatHistoryAggregator.Core.Tests.AdaptersTests;

public class EventServiceTests
{
    private readonly IEventService _eventService;
    private readonly Mock<IEventRepository> _eventRepositoryMock;

    public EventServiceTests()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _eventService = new EventService(_eventRepositoryMock.Object);
    }

    [Fact]
    public void GetEvents_Should_Return_Events_From_Repository()
    {
        // Arrange
        var chatEvents = new List<ChatEvent>
        {
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 10, 0), Type = EventType.Comment, UserName = "User1", Message = "Hello!" },
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 5, 0), Type = EventType.Enter, UserName = "User2" },
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 20, 0), Type = EventType.Leave, UserName = "User3" }
        };

        _eventRepositoryMock.Setup(er => er.GetEvents()).Returns(chatEvents);

        // Act
        var events = _eventService.GetEvents();

        // Assert
        events.Should().NotBeNullOrEmpty();
        events.Count.Should().Be(3);
        events.First().UserName.Should().Be("User1");
        events.Last().UserName.Should().Be("User3");
    }
}