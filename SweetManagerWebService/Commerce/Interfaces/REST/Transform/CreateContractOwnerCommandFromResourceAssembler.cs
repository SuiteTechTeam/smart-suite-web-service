using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Domain.Model.ValueObjects;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

public static class CreateContractOwnerCommandFromResourceAssembler
{
    public static CreateContractOwnerCommand ToCommandFromResource(CreateContractOwnerResource resource)
    {
        if (!Enum.TryParse<EStates>(resource.Status, true, out var status))
        {
            throw new ArgumentException($"Invalid value for Status: {resource.Status}");
        }
        
        return new CreateContractOwnerCommand(
            resource.OwnerId,
            resource.StartDate,
            resource.FinalDate,
            resource.SubscriptionId,
            status);
    }
}