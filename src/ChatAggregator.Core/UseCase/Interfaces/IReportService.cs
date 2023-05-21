using ChatAggregator.Domain.Enums;

namespace ChatAggregator.Core.UseCase.Interfaces;

public interface IReportService
{
    string RenderReport(Granularity granularity);
}
