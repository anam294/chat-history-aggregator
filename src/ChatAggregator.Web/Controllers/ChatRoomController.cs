using ChatAggregator.Core.UseCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatAggregator.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatRoomController : ControllerBase
{
    private readonly IFetcherService _chatsFetcher;
    
    public ChatRoomController(IFetcherService fetcher)
    {
        _chatsFetcher = fetcher;
    }
    
    [HttpGet(Name = "GetAggregatedChatsByGranularity")]
    public string Get(string granularity)
    {
        return _chatsFetcher.Fetch(granularity);
    }
}