using ChatAggregator.Core.Configurations;
using ChatAggregator.Core.UseCase.InterfaceAdapters;
using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Entities;
using ChatAggregator.Domain.Enums;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace ChatHistoryAggregator.Core.Tests.AdaptersTests;

public class EventRepositoryTests
{
    private readonly IEventRepository _eventRepository;
    private readonly Mock<IDataService> _dataServiceMock;
    private readonly AppSettings _appSettings;
    
    public EventRepositoryTests()
    {
        _appSettings = new AppSettings { DataFilePath = "test.json" };
        _dataServiceMock = new Mock<IDataService>();
        _eventRepository = new EventRepository(_appSettings, _dataServiceMock.Object);
    }
    
    [Fact]
    public void GetEvents_Should_Return_Events_In_Order()
    {
        // Arrange
        var chatEvents = new List<ChatEvent>
        {
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 10, 0), Type = EventType.Comment, UserName = "User3", Message = "Hello!" },
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 5, 0), Type = EventType.Enter, UserName = "User1" },
            new ChatEvent { Time = new DateTime(2023, 5, 15, 5, 20, 0), Type = EventType.Leave, UserName = "User2" }
        };
        var jsonString = JsonConvert.SerializeObject(chatEvents);
        _dataServiceMock.Setup(ds => ds.ReadAllText(_appSettings.DataFilePath)).Returns(jsonString);

        // Act
        var events = _eventRepository.GetEvents();

        // Assert
        events.Should().NotBeNullOrEmpty();
        events.Count.Should().Be(3);
        events.First().UserName.Should().Be("User1");
        events.Last().UserName.Should().Be("User2");
    }

    
    [Fact]
    public void GetEvents_Should_Throw_When_Invalid_Json()
    {
        // Arrange
        var invalidJson = "invalid";
        _dataServiceMock.Setup(ds => ds.ReadAllText(_appSettings.DataFilePath)).Returns(invalidJson);

        // Act
        Action act = () => _eventRepository.GetEvents();

        // Assert
        act.Should().Throw<JsonException>();
    }
}
