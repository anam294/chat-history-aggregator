namespace ChatAggregator.Domain.Entities;

public class ChatEvent
{
    public enum EventType
    {
        Enter,
        Leave,
        Comment,
        HighFive
    }

    public DateTime Time { get; set; }
    public string UserName { get; set; }
    public EventType Type { get; set; }
    public string TargetUser { get; set; }
    public string Message { get; set; }
}