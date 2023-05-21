using ChatAggregator.Core.UseCase.Interfaces;
using ChatAggregator.Domain.Enums;

namespace ChatAggregator.App.UseCase.InterfaceAdapters;

public class FetcherService: IFetcherService
{
    private readonly IReportService _reportService;
    
    public FetcherService(IReportService reportService)
    {
        _reportService = reportService;
    }
    public string Fetch(string granularity)
    {
        var status = Enum.TryParse(granularity, true, out Granularity aggregationLevel);
        return _reportService.RenderReport(aggregationLevel);
    }
}