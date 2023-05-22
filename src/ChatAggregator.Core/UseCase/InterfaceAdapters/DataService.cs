using ChatAggregator.Core.UseCase.Interfaces;

namespace ChatAggregator.Core.UseCase.InterfaceAdapters;

public class DataService : IDataService
{
    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }
}