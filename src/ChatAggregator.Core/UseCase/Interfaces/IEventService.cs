using ChatAggregator.Domain.Entities;

namespace ChatAggregator.Core.UseCase.Interfaces;

public interface IEventService
{
    List<ChatEvent> GetEvents();
}
