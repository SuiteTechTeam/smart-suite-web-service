using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Interfaces.REST.Resources;

namespace SweetManagerIotWebService.API.Commerce.Interfaces.REST.Transform;

public static class PaymentOwnerResourceFromEntityAssembler
{
    public static PaymentOwnerResource ToResourceFromEntity(PaymentOwner paymentOwner)
    {
        return new PaymentOwnerResource(
            paymentOwner.Id,
            paymentOwner.OwnerId,
            paymentOwner.Description,
            paymentOwner.FinalAmount);
    }
}