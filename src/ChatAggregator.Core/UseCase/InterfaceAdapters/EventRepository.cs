using ChatAggregator.Core.Configurations;
using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Entities;
using Newtonsoft.Json;

namespace ChatAggregator.Core.UseCase.InterfaceAdapters;

public class EventRepository : IEventRepository
{
    private readonly AppSettings _appSettings;
    public EventRepository(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }
    public List<ChatEvent> GetEvents()
    {
        var jsonString = File.ReadAllText(_appSettings.DataFilePath);
        var events = JsonConvert.DeserializeObject<List<ChatEvent>>(jsonString);
        return events.OrderBy(e => e.Time).ToList();
    }
}
