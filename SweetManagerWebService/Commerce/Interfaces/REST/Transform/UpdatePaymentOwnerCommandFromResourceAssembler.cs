using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

public static class UpdatePaymentOwnerCommandFromResourceAssembler
{
    public static UpdatePaymentOwnerCommand ToCommandFromResource(UpdatePaymentOwnerResource resource)
    {
        return new UpdatePaymentOwnerCommand(
            resource.Id,
            resource.OwnerId,
            resource.Description,
            resource.FinalAmount);
    }
}