using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Entities;
using Newtonsoft.Json;

namespace ChatAggregator.App.UseCase.InterfaceAdapters;

public class EventRepository : IEventRepository
{
    private readonly string _filePath = @"/Users/anamirfan/Documents/Projects/ChatHistoryAggregator/src/ChatAggregator.Core/Chats.json";

    public List<ChatEvent> GetEvents()
    {
        var jsonString = File.ReadAllText(_filePath);
        var events = JsonConvert.DeserializeObject<List<ChatEvent>>(jsonString);
        return events.OrderByDescending(e => e.Time).ToList();
    }
}
