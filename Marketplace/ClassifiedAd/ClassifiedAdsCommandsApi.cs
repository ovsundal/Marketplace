using Marketplace.ClassifiedAd;
using static Marketplace.ClassifiedAd.Contracts;
using Microsoft.AspNetCore.Mvc;
using Serilog;



[Route("/ad")]
public class ClassifiedAdsCommandsApi : Controller
{
    private readonly ClassifiedAdsApplicationService _applicationService;

    public ClassifiedAdsCommandsApi(ClassifiedAdsApplicationService applicationService)
        => _applicationService = applicationService;

    [HttpPost]
    public Task<IActionResult> Post(
        V1.Create request) => HandleRequest(request, _applicationService.Handle);

    [Route("name")]
    [HttpPut]
    public Task<IActionResult> Put(V1.SetTitle request) => HandleRequest(request, _applicationService.Handle);

    [Route("text")]
    [HttpPut]
    public Task<IActionResult> Put(V1.UpdateText request) => HandleRequest(request, _applicationService.Handle);

    [Route("price")]
    [HttpPut]
    public Task<IActionResult> Put(V1.UpdatePrice request) => HandleRequest(request, _applicationService.Handle);

    [Route("publish")]
    [HttpPut]
    public Task<IActionResult> Put(V1.RequestToPublish request) => HandleRequest(request, _applicationService.Handle);

    private async Task<IActionResult> HandleRequest<T>(T request, Func<T, Task> handler)
    {
        try
        {
            Log.Debug("Handling HTTP request of type {type}", typeof(T).Name);
            await handler(request);
            return Ok();
        }
        catch (Exception e)
        {
            Log.Error(e, "Error handling the request");
            return new BadRequestObjectResult(new { error = e.Message, stackTrace = e.StackTrace });
        }
    }
}