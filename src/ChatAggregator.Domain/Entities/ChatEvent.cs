using ChatAggregator.Domain.Enums;

namespace ChatAggregator.Domain.Entities;

public class ChatEvent
{
    public DateTime Time { get; set; }
    public string UserName { get; set; }
    public EventType Type { get; set; }
    public string TargetUser { get; set; }
    public string Message { get; set; }
}