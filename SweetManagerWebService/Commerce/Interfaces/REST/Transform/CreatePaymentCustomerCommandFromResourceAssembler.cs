using SweetManagerIotWebService.API.Commerce.Domain.Model.Commands;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

public static class CreatePaymentCustomerCommandFromResourceAssembler
{
    public static CreatePaymentCustomerCommand ToCommandFromResource(CreatePaymentCustomerResource resource)
    {
        return new CreatePaymentCustomerCommand(
            resource.GuestId,
            resource.FinalAmount);
    }
}