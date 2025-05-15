using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

public static class CreatePaymentOwnerCommandFromResourceAssembler
{
    public static CreatePaymentOwnerCommand ToCommandFromResource(CreatePaymentOwnerResource resource)
    {
        return new CreatePaymentOwnerCommand(
            resource.OwnerId,
            resource.Description,
            resource.FinalAmount);
    }
}