using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Entities;

namespace ChatAggregator.Core.UseCase.InterfaceAdapters;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public List<ChatEvent> GetEvents()
    {
        return _eventRepository.GetEvents();
    }
}
