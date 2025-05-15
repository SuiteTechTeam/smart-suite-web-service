using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

public static class PaymentCustomerResourceFromEntityAssembler
{
    public static PaymentCustomerResource ToResourceFromEntity(PaymentCustomer paymentCustomer)
    {
        return new PaymentCustomerResource(
            paymentCustomer.Id,
            paymentCustomer.GuestId,
            paymentCustomer.FinalAmount);
    }
}