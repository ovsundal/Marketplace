using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api;

[Route("/ad")]
public class ClassifiedAdsCommandsApi : Controller
{
    private readonly ClassifiedAdApplicationService _applicationService;
    public ClassifiedAdsCommandsApi(ClassifiedAdApplicationService applicationService)
        => _applicationService = applicationService;

    [HttpPost]
    public async Task<IActionResult> Post(
        Contracts.ClassifiedAds.V1.Create request)
    {
        _applicationService.Handle(request);

        return Ok();
    }
}