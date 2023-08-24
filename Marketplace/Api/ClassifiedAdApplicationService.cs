using Marketplace.Contracts;
using Marketplace.Framework;

namespace Marketplace.Api;

public class ClassifiedAdApplicationService : IApplicationService
{
    public Task Handle(object command)
    {
        switch (command)
        {
            case ClassifiedAds.V1.Create cmd:
                // create new Classified ad here
                break;
            
            default:
                throw new InvalidOperationException($"Command type {command.GetType().FullName} is unknown");
        }
        throw new NotImplementedException();
    }
}