using System.Text;
using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Entities;
using ChatAggregator.Domain.Enums;

namespace ChatAggregator.Core.UseCase.InterfaceAdapters;

public class ReportService : IReportService
{
    private readonly IEventService _eventService;

    public ReportService(IEventService eventService)
    {
        _eventService = eventService;
    }

    public string RenderReport(Granularity granularity)
    {
        var events = _eventService.GetEvents();
        var stringBuilder = new StringBuilder();

        switch (granularity)
        {
            case Granularity.Minute:
                var groupedByMinute = events.GroupBy(e => new { e.Time.Date, e.Time.Hour, e.Time.Minute });

                foreach (var group in groupedByMinute)
                {
                    foreach (var chatEvent in group)
                    {
                        stringBuilder.AppendLine(chatEvent.Time.ToString() + ": " + GetEventDescription(chatEvent));
                    }
                }
                break;

            case Granularity.Hourly:
                var groupedByHour = events.GroupBy(e => new { e.Time.Date, e.Time.Hour });

                foreach (var group in groupedByHour)
                {
                    stringBuilder.AppendLine($"{group.Key.Date.ToString("dd/MM/yyyy")} - {group.Key.Hour}:00");
                    AppendAggregatedEvents(group.ToList(), stringBuilder);
                }
                break;

            case Granularity.Daily:
                var groupedByDay = events.GroupBy(e => e.Time.Date);

                foreach (var group in groupedByDay)
                {
                    stringBuilder.AppendLine($"{group.Key.Date}");
                    AppendAggregatedEvents(group.ToList(), stringBuilder);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(granularity), granularity, null);
        }

        return stringBuilder.ToString();
    }

    private string GetEventDescription(ChatEvent chatEvent)
    {
        switch (chatEvent.Type)
        {
            case EventType.Enter:
                return $"{chatEvent.UserName} enters the room";
            case EventType.Leave:
                return $"{chatEvent.UserName} leaves";
            case EventType.Comment:
                return $"{chatEvent.UserName} comments: \"{chatEvent.Message}\"";
            case EventType.HighFive:
                return $"{chatEvent.UserName} high-fives {chatEvent.TargetUser}";
            default:
                throw new Exception("Unknown event type");
        }
    }

    private void AppendAggregatedEvents(List<ChatEvent> group, StringBuilder stringBuilder)
    {
        var enters = group.Count(e => e.Type == EventType.Enter);
        var leaves = group.Count(e => e.Type == EventType.Leave);
        var comments = group.Count(e => e.Type == EventType.Comment);
        var highFives = group.Count(e => e.Type == EventType.HighFive);

        if (enters > 0)
            stringBuilder.AppendLine($"\t{enters} person(s) entered");
        if (leaves > 0)
            stringBuilder.AppendLine($"\t{leaves} person(s) left");
        if (highFives > 0)
            stringBuilder.AppendLine($"\t{highFives} high-five(s)");
        if (comments > 0)
            stringBuilder.AppendLine($"\t{comments} comment(s)");
    }
}
