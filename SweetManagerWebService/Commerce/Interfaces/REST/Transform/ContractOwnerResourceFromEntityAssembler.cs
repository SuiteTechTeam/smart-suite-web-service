using Microsoft.OpenApi.Extensions;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Entities;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

public static class ContractOwnerResourceFromEntityAssembler
{
    public static ContractOwnerResource ToResourceFromEntity(ContractOwner contractOwner)
    {
        return new ContractOwnerResource(
            contractOwner.Id,
            contractOwner.OwnerId,
            contractOwner.StartDate,
            contractOwner.FinalDate,
            contractOwner.SubscriptionId,
            contractOwner.Status.GetDisplayName());
    }
}