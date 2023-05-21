namespace ChatAggregator.Core.UseCase.Interfaces;

public interface IFetcherService
{
    string Fetch(string granularity);
}