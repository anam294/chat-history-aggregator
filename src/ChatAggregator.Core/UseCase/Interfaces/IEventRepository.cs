using ChatAggregator.Domain.Entities;

namespace ChatAggregator.Core.UseCase.Interfaces;

public interface IEventRepository
{
    List<ChatEvent> GetEvents();
}
