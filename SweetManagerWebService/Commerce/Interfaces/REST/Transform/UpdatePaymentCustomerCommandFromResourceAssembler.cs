using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

public static class UpdatePaymentCustomerCommandFromResourceAssembler
{
    public static UpdatePaymentCustomerCommand ToCommandFromResource(UpdatePaymentCustomerResource resource)
    {
        return new UpdatePaymentCustomerCommand(
            resource.Id,
            resource.GuestId,
            resource.FinalAmount);
    }
}