using ChatAggregator.Core.Configurations;
using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Entities;
using Newtonsoft.Json;

namespace ChatAggregator.Core.UseCase.InterfaceAdapters;

public class EventRepository : IEventRepository
{
    private readonly AppSettings _appSettings;
    private readonly IDataService _dataService;
    public EventRepository(AppSettings appSettings, IDataService dataService)
    {
        _appSettings = appSettings;
        _dataService = dataService; 
    }
    public List<ChatEvent> GetEvents()
    {
        var jsonString = _dataService.ReadAllText(_appSettings.DataFilePath);
        var events = JsonConvert.DeserializeObject<List<ChatEvent>>(jsonString);
        return events.OrderBy(e => e.Time).ToList();
    }
}
